using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    public class CompKirin : ThingComp
    {
        public CompProperties_Kirin Props => (CompProperties_Kirin)props;
        public Pawn GetPawn => parent as Pawn;

        public override void CompTick()
        {
            base.CompTick();
            if (GetPawn == null || GetPawn.Dead || GetPawn.Map == null ) return;
            if (GetPawn.IsHashIntervalTick(GenTicks.SecondsToTicks(Props.secondsBetweenLightning)) && (GetPawn.IsFighting() || GetPawn.InAggroMentalState))
            {
                IntVec3 strikeLocation = CellFinderLoose.RandomCellWith((IntVec3 sq) => sq.Standable(GetPawn.Map) && !GetPawn.Map.roofGrid.Roofed(sq) && GetPawn.Position.DistanceTo(sq) <= Props.lightningRadius, GetPawn.Map, 1000);
                Find.CurrentMap.weatherManager.eventHandler.AddEvent(new WeatherEvent_LightningStrike(GetPawn.Map, strikeLocation));
            }
        }
    }
}
