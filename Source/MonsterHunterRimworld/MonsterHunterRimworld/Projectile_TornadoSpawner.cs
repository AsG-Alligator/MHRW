using RimWorld;
using Verse;

namespace MonsterHunterRimworld
{
    public class Projectile_TornadoSpawner : Projectile
    {
        public ThingDef_TornadoSpawner Def
        {
            get
            {
                return this.def as ThingDef_TornadoSpawner;
            }
        }

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            IntVec3 position = base.Position;
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Tornado, null), position, base.Map, WipeMode.Vanish);
            base.Impact(hitThing);
        }

        public override void Tick()
        {
            base.Tick();
            FleckMaker.ThrowDustPuff(base.Position, base.Map, 1.0f);
        }


    }


    public class ThingDef_TornadoSpawner : ThingDef
    {

    }




}
