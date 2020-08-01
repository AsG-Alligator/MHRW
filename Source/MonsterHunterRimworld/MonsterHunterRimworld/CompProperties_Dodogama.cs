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

        public float maxNutritionFromRock = 0.5f;
        public float timeToEatInSeconds = 2f;
    }
}
