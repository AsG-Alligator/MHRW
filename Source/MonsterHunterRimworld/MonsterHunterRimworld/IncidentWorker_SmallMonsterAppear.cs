using RimWorld;
using UnityEngine;
using Verse;

namespace MonsterHunterRimworld
{
    class IncidentWorker_SmallMonsterAppear : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            if (WyvernUtility.GetRandomSmallMonsterForEvent(map) == null) return false;
            IntVec3 intVec;
            return RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef smallMonster = WyvernUtility.GetRandomSmallMonsterForEvent(map);
            if (smallMonster == null) return false;
            IntVec3 intVec;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null))
            {
                return false;
            }
            int freeColonistsCount = map.mapPawns.FreeColonistsCount;
            float randomInRange = IncidentWorker_SmallMonsterAppear.CountPerColonistRange.RandomInRange;
            float f = (float)freeColonistsCount * randomInRange;
            int num = Mathf.Clamp(GenMath.RoundRandom(f), 1, 10);
            for (int i = 0; i < num; i++)
            {
                IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
                Pawn newThing = PawnGenerator.GeneratePawn(smallMonster, null);
                Pawn newSmallMonster = (Pawn)GenSpawn.Spawn(newThing, loc, map, WipeMode.Vanish);
                newSmallMonster.needs.food.CurLevelPercentage = 1f;
            }
            Find.LetterStack.ReceiveLetter("LetterLabelSmallMonsterAppear".Translate(), "LetterTextSmallMonsterAppear".Translate(smallMonster.label.CapitalizeFirst()), LetterDefOf.NeutralEvent, new TargetInfo(intVec, map, false), null, null);
            return true;
        }

        private static readonly FloatRange CountPerColonistRange = new FloatRange(0.5f, 1.5f);
    }
}
