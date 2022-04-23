using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using EquipmentToolbox;

namespace MHRW_Weapons
{
    class PostBlockEffecter : SpecialEffectsUtility
    {

        public override void DoPostBlockEvent(Pawn pawn, bool successfullyBlocked, ThingWithComps shield)
        {
            if (successfullyBlocked)
            {
                Effecter effecter = effecter = EffecterDefOf.Mine.Spawn();
                IntVec3 position = pawn.Position;
                effecter.Trigger(pawn, pawn);
            }
        }
    }
}
