using RimWorld;
using Verse;
using System.Collections.Generic;
using System.Linq;



namespace MonsterHunterRimworld
{
    public class Projectile_PoisonBurst : Projectile_Explosive
    {

        public void applyHediffToPawnsInArea(Map map, HashSet<IntVec3> AoE, HediffDef hediff, BodyPartRecord bodyPartRecord, DamageInfo? dinfo = null, DamageWorker.DamageResult damageResult = null)
        {
            foreach (Pawn pawn in map.listerThings.AllThings.FindAll(c => c is Pawn && AoE.Contains(c.Position)))
            {
                if (pawn != null && hediff !=null)
                {
                    Log.Message(hediff.defName + " , " + pawn.ThingID);
                    Hediff h = HediffMaker.MakeHediff(hediff, pawn, null);                     
                    pawn.health.AddHediff(h, bodyPartRecord, dinfo, damageResult);
                }
            }
        }
        public ThingDef_PoisonBurst Def
        {
            get
            {
                return this.def as ThingDef_PoisonBurst;
            }
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            applyHediffToPawnsInArea(this.Map, GenRadial.RadialCellsAround(this.Position, this.def.projectile.explosionRadius, true).ToHashSet(), HediffDefOf.ToxicBuildup, null);
            base.Impact(hitThing); 
        }
    }

    public class ThingDef_PoisonBurst : ThingDef
    {

    }

}