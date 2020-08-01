using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    public class CompProperties_Dodogama : CompProperties
    {
        public CompProperties_Dodogama()
        {
            this.compClass = typeof(CompDodogama);
        }

        public float maxNutritionPercent = 0.5f;
        public ThingCategoryDef foodThingCategoryDef = ThingCategoryDefOf.StoneChunks;
    }
}
