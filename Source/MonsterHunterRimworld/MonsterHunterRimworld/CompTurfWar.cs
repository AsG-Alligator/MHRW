using RimWorld;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MonsterHunterRimworld
{
    public class CompTurfWar : ThingComp
    {
        public CompProperties_TurfWar Props
        {
            get
            {
                return (CompProperties_TurfWar)props;
            }
        }

        public override void CompTickRare()
        {
            base.CompTickRare();

            
            if (!(parent is Pawn largeMonster)) return;
            if (largeMonster.Faction != null) return;
            Map map = largeMonster.Map;
            if (map == null) return;
            if (largeMonster.IsFighting() || largeMonster.Downed || largeMonster.Dead) return;

            List<Pawn> possibleTargetsToFight = map.mapPawns.AllPawnsSpawned.FindAll(p => p.RaceProps.Animal && p.Faction == null && !p.IsFighting() && !p.Downed && !p.Dead && !WyvernUtility.IsSameSpecies(p, largeMonster) && p.Position.DistanceTo(largeMonster.Position) < Props.maxFightRange && p.TryGetComp<CompTurfWar>() != null);
            if (possibleTargetsToFight.Count == 0) return;
            Pawn targetToFight = possibleTargetsToFight.RandomElement();

            if (largeMonster.jobs.TryTakeOrderedJob(new Job(JobDefOf.AttackMelee, targetToFight))) Messages.Message("TurfWarLabel".Translate().CapitalizeFirst(), largeMonster, MessageTypeDefOf.NeutralEvent);
            if (targetToFight.jobs.TryTakeOrderedJob(new Job(JobDefOf.AttackMelee, largeMonster))) Messages.Message("TurfWarLabel".Translate().CapitalizeFirst(), targetToFight, MessageTypeDefOf.NeutralEvent);
        }
    }
}
