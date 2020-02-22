using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MonsterHunterRimworld
{
    class IncidentWorker_ElderDragonAppear : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            IntVec3 intVec;
            return RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef elderDragon = WyvernUtility.GetRandomPawnKindForWyvernType("ELDERDRAGON");
            IntVec3 intVec;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null))
            {
                return false;
            }
            IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
            Pawn newThing = PawnGenerator.GeneratePawn(elderDragon, null);
            Pawn newElderDragon = (Pawn)GenSpawn.Spawn(newThing, loc, map, WipeMode.Vanish);
            newElderDragon.needs.food.CurLevelPercentage = 1f;
            List<Pawn> wildlifeAnimals = map.mapPawns.AllPawnsSpawned.FindAll(p => p != newElderDragon && p.Faction == null && p.RaceProps.Animal && !p.health.Dead);
            foreach (Pawn wildlifeAnimal in wildlifeAnimals)
            {
                if (Rand.Chance(0.2f)) continue;
                if (WyvernUtility.IsElderDragon(wildlifeAnimal))
                {
                    if (newElderDragon.RaceProps.AnyPawnKind != wildlifeAnimal.RaceProps.AnyPawnKind)
                    {
                        newElderDragon.jobs.TryTakeOrderedJob(new Job(JobDefOf.AttackMelee, wildlifeAnimal)); // newly arriving elder dragon attacks currently present elder dragon
                        if (wildlifeAnimal.jobs.curJob.def == JobDefOf.AttackMelee) continue; // currently present elder dragon does not fight new elder dragon if already fighting
                        wildlifeAnimal.jobs.TryTakeOrderedJob(new Job(JobDefOf.AttackMelee, newElderDragon));
                    }
                    continue;
                }
                JobGiver_ExitMapPanic jobFlee = new JobGiver_ExitMapPanic();
                ThinkResult thinkResult = jobFlee.TryIssueJobPackage(wildlifeAnimal, new JobIssueParams() { maxDistToSquadFlag = 500});
                wildlifeAnimal.jobs.TryTakeOrderedJob(thinkResult.Job);
            }
            Find.LetterStack.ReceiveLetter("LetterLabelElderDragonAppear".Translate(), "LetterTextElderDragonAppear".Translate(newElderDragon.Label.CapitalizeFirst()), LetterDefOf.ThreatSmall, new TargetInfo(intVec, map, false), null, null);
            return true;
        }
    }
}
