using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    public class CompProperties_Kirin : CompProperties
    {
        public CompProperties_Kirin()
        {
            this.compClass = typeof(CompKirin);
        }

        public float secondsBetweenLightning = 10f;
        public float lightningRadius = 15f;
    }
}
