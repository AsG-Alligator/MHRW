using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    public class CompDelex : ThingComp
    {
        public CompProperties_Delex Props => (CompProperties_Delex)props;
        public Pawn GetPawn => parent as Pawn;

        public override void CompTickRare()
        {
            base.CompTickRare();
            if(GetPawn.Position.GetTerrain(GetPawn.Map)==TerrainDefOf.Sand || GetPawn.Position.GetTerrain(GetPawn.Map) == MHRWDefOf.SoftSand)
            {
                Hediff sandglide = HediffMaker.MakeHediff(MHRWDefOf.DelexSand, GetPawn, null);
                GetPawn.health.AddHediff(sandglide);
            }
            else 
            {
                GetPawn.health.hediffSet.hediffs.RemoveAll(x => x.def == MHRWDefOf.DelexSand);
            }
        }
    }

    public class CompProperties_Delex : CompProperties
    {
        public CompProperties_Delex()
        {
            this.compClass = typeof(CompDelex);
        }
    }
}
