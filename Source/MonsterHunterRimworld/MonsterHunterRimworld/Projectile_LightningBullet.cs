using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    public class Projectile_LightningBullet: Projectile_Explosive
    {
        public ThingDef_LightningBullet Def
        {
            get
            {
                return this.def as ThingDef_LightningBullet;
            }
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            IntVec3 strikeLocation = base.Position;
            Find.CurrentMap.weatherManager.eventHandler.AddEvent(new WeatherEvent_LightningStrike(this.Map, strikeLocation));
            base.Impact(hitThing);
        }
    }
}
