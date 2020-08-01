using Verse;

namespace MonsterHunterRimworld
{
    public class MonsterHunterRimworldModSettings : ModSettings
    {
        public float minElderDragonEventDelay = 60f;
        public bool elderDragonWeatherEffects = true;
        public bool elderDragonScareAnimals = true;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref minElderDragonEventDelay, "minElderDragonEventDelay", 60f, false);
            Scribe_Values.Look<bool>(ref elderDragonWeatherEffects, "elderDragonWeatherEffects", true, false);
            Scribe_Values.Look<bool>(ref elderDragonScareAnimals, "elderDragonScareAnimals", true, false);
        }
    }
}
