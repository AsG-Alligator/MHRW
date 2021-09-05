using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    class Projectile_Motetrail : Projectile_Explosive
    {
        public ThingDef_Projectile_Motetrail Def
        {
            get
            {
                return this.def as ThingDef_Projectile_Motetrail;
            }
        }
        public override void Tick()
        {
            base.Tick();
            FleckMaker.ThrowDustPuff(base.Position, base.Map, 1.0f);
        }
    }
    public class ThingDef_Projectile_Motetrail : ThingDef
    {

    }
}
