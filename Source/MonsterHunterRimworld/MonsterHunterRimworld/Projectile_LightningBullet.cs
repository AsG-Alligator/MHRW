using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    class Projectile_LightningBullet: Bullet
    {
        public ThingDef_LightningBullet Def
        {
            get
            {
                return this.def as ThingDef_LightningBullet;
            }
        }

        protected override void Impact(Thing hitThing)
        {
            IntVec3 strikeLocation = base.Position;

            Find.CurrentMap.weatherManager.eventHandler.AddEvent(new WeatherEvent_LightningStrike(this.Map, strikeLocation));
            base.Impact(hitThing);
        }
    }


}
