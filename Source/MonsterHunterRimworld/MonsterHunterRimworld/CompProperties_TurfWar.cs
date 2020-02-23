using Verse;

namespace MonsterHunterRimworld
{
    public class CompProperties_TurfWar : CompProperties
    {
        public CompProperties_TurfWar()
        {
            this.compClass = typeof(CompTurfWar);
        }

        public float maxFightRange = 20;
    }
}
