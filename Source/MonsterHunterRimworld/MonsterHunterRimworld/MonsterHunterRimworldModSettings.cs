using Verse;

namespace MonsterHunterRimworld
{
    public class MonsterHunterRimworldModSettings : ModSettings
    {
        public float minElderDragonEventDelay = 60f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref minElderDragonEventDelay, "minElderDragonEventDelay", 60f, false);
        }
    }
}
