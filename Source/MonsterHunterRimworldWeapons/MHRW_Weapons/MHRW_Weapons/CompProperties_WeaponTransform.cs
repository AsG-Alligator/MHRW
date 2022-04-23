using Verse;

namespace MHRW_Weapons
{
    public class CompProperties_WeaponTransform : CompProperties
    {
        public string TransformLabel = "";
        public string TransformDesc;
        public string TransformIcon;
        public SoundDef transformSound;
        public ThingDef transformInto;
        public ThingDef transformSecondaryProduct = null;
        public float transformTime = 0f;
        public ThingDef usesAmmunition = null;
        public ThingDef needSpecialItemEquipped = null;

        public CompProperties_WeaponTransform()
        {
            compClass = typeof(CompWeaponTransform);
        }
    }
}
