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
            if (GenTicks.TicksGame < GenDate.TicksPerDay * LoadedModManager.GetMod<MonsterHunterRimworldMod>().GetSettings<MonsterHunterRimworldModSettings>().minElderDragonEventDelay) return false;
            Map map = (Map)parms.target;
            if (WyvernUtility.GetRandomElderDragonForEvent(map) == null) return false;
            IntVec3 intVec;
            return RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef elderDragon = WyvernUtility.GetRandomElderDragonForEvent(map);
            if (elderDragon == null) return false;
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
                if (Rand.Chance(0.2f)) continue; // animals will flee with a 80% chance
                if (WyvernUtility.IsElderDragon(wildlifeAnimal))
                {
                    if (!WyvernUtility.IsSameSpecies(newElderDragon, wildlifeAnimal))
                    {
                        if (wildlifeAnimal.IsFighting()) continue;
                        if (newElderDragon.jobs.TryTakeOrderedJob(new Job(JobDefOf.AttackMelee, wildlifeAnimal))) Messages.Message("TurfWarLabel".Translate().CapitalizeFirst(), newElderDragon, MessageTypeDefOf.NeutralEvent);
                        if (wildlifeAnimal.jobs.TryTakeOrderedJob(new Job(JobDefOf.AttackMelee, newElderDragon))) Messages.Message("TurfWarLabel".Translate().CapitalizeFirst(), wildlifeAnimal, MessageTypeDefOf.NeutralEvent);
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
