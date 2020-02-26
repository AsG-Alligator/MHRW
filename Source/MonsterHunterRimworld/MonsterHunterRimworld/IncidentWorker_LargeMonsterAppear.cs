using RimWorld;
using UnityEngine;
using Verse;

namespace MonsterHunterRimworld
{
    class IncidentWorker_LargeMonsterAppear : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            Map map = (Map)parms.target;
            if (WyvernUtility.GetRandomLargeMonsterForEvent(map) == null) return false;
            IntVec3 intVec;
            return RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            PawnKindDef largeMonster = WyvernUtility.GetRandomLargeMonsterForEvent(map);
            if (largeMonster == null) return false;
            IntVec3 intVec;
            if (!RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, CellFinder.EdgeRoadChance_Animal, false, null))
            {
                return false;
            }
            IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
            Pawn newThing = PawnGenerator.GeneratePawn(largeMonster, null);
            PawnKindDef smallMonsterGroup = WyvernUtility.GetSmallMonsterGroupForLargeMonster(largeMonster); // is leader of pack if not null
            if (smallMonsterGroup != null) newThing.gender = Gender.Male; // pack leader are always male
            Pawn newLargeMonster = (Pawn)GenSpawn.Spawn(newThing, loc, map, WipeMode.Vanish);
            newLargeMonster.needs.food.CurLevelPercentage = 1f;
            
            if (smallMonsterGroup == null)
            {
                Find.LetterStack.ReceiveLetter("LetterLabelLargeMonsterAppear".Translate(), "LetterTextLargeMonsterAppear".Translate(newLargeMonster.Label.CapitalizeFirst()), LetterDefOf.ThreatSmall, new TargetInfo(intVec, map, false), null, null);
                return true;
            }

            int freeColonistsCount = map.mapPawns.FreeColonistsCount;
            float randomInRange = IncidentWorker_LargeMonsterAppear.CountPerColonistRange.RandomInRange;
            float f = (float)freeColonistsCount * randomInRange;
            int num = Mathf.Clamp(GenMath.RoundRandom(f), 1, 10);
            for (int i = 0; i < num; i++)
            {
                Pawn newThing2 = PawnGenerator.GeneratePawn(smallMonsterGroup, null);
                Pawn newSmallMonster = (Pawn)GenSpawn.Spawn(newThing2, loc, map, WipeMode.Vanish);
                newSmallMonster.needs.food.CurLevelPercentage = 1f;
            }
            Find.LetterStack.ReceiveLetter("LetterLabelLargeMonsterAppear".Translate(), "LetterTextLargeMonsterAppearWithGroup".Translate(newLargeMonster.Label.CapitalizeFirst(), smallMonsterGroup.label.CapitalizeFirst()), LetterDefOf.ThreatSmall, new TargetInfo(intVec, map, false), null, null);

            return true;
        }

        private static readonly FloatRange CountPerColonistRange = new FloatRange(0.5f, 1.2f);
    }
}
