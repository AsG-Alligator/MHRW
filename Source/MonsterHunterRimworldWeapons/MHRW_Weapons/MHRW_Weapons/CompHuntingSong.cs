using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace MHRW_Weapons
{
    public class CompHuntingSong : ThingComp
    {
        public void PlaySong(HediffDef hediff, float radius)
        {
            Pawn parentPawn = parent as Pawn;
            HashSet<IntVec3> AoE = GenRadial.RadialCellsAround(parentPawn.Position, radius, true).ToHashSet();
            foreach (Pawn pawn in parentPawn.Map.listerThings.AllThings.FindAll(c => c is Pawn && AoE.Contains(c.Position)))
            {
                if (pawn != null && hediff != null)
                {
                    if (pawn.Faction == parentPawn.Faction)
                    {
                        //Log.Message(hediff.defName + " , " + pawn.ThingID);
                        Hediff h = HediffMaker.MakeHediff(hediff, pawn, null);
                        pawn.health.AddHediff(h);
                    }
                }
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            return base.CompGetGizmosExtra();
        }

    }

}
