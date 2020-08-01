using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MonsterHunterRimworld
{
    public class CompDodogama : ThingComp
    {
        public CompProperties_Dodogama Props => (CompProperties_Dodogama)props;
        public Pawn GetPawn => parent as Pawn;

        public override void CompTick()
        {
            base.CompTick();
            cooldownTicks--;
            if (GetPawn == null || GetPawn.Dead || cooldownTicks > 0 || GetPawn.Map == null || GetPawn.CurJobDef != JobDefOf.Ingest) return;

            List<Thing> things = new List<Thing>();
            foreach (ThingDef thingDef in GetPawn.Map.GetDirectlyHeldThings().ToList().FindAll(t => t.def.thingCategories != null && t.def.thingCategories.Contains(Props.foodThingCategoryDef)).Select(d => d.def).Distinct())
            {
                Thing thing = GenClosest.ClosestThingReachable(GetPawn.Position, GetPawn.Map, ThingRequest.ForDef(thingDef), Verse.AI.PathEndMode.Touch, TraverseParms.For(GetPawn, Danger.Deadly, TraverseMode.ByPawn, false));
                things.Add(thing);
            }
            if (things.Count == 0)
            {
                cooldownTicks = GenDate.TicksPerDay / 2;
                return;
            }
            // TODO add job
            Log.Warning("do Dodogama eat rocks job");
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<int>(ref cooldownTicks, "cooldown", -9999, false);
        }

        public int cooldownTicks = -9999;
    }
}
