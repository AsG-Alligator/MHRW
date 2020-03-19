using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MonsterHunterRimworld
{
    public class CompWeaponTransform : CompUseEffect
    {
        public CompProperties_WeaponTransform Props => (CompProperties_WeaponTransform)props;

        public CompEquippable GetEquippable => parent.GetComp<CompEquippable>();
        public Pawn GetPawn => GetEquippable.verbTracker.PrimaryVerb.CasterPawn;

        public void Transform()
        {
            CompQuality compQualityOld = parent.TryGetComp<CompQuality>();
            int hitPoints = parent.HitPoints;
            ThingComp compInfusedOld = parent.AllComps.Find(c => c.GetType().ToString() == "Infused.CompInfused");

            ThingWithComps weaponToCreate = (ThingWithComps)ThingMaker.MakeThing(Props.transformInto, parent.Stuff);
            weaponToCreate.HitPoints = hitPoints;
            if (compQualityOld != null && weaponToCreate.TryGetComp<CompQuality>() is CompQuality compQualityNew)
            {
                weaponToCreate.AllComps.Remove(compQualityNew);
                weaponToCreate.AllComps.Add(compQualityOld);
            }
            if (compInfusedOld != null && weaponToCreate.AllComps.Find(c => c.GetType().ToString() == "Infused.CompInfused") is ThingComp compInfusedNew)
            {
                weaponToCreate.AllComps.Remove(compInfusedNew);
                weaponToCreate.AllComps.Add(compInfusedOld);
            }

            ThingWithComps weaponToCreateSecondary = null;
            if (Props.transformSecondaryProduct != null)
            {
                weaponToCreateSecondary = (ThingWithComps)ThingMaker.MakeThing(Props.transformSecondaryProduct, parent.Stuff);
                weaponToCreateSecondary.HitPoints = hitPoints;
                if (compQualityOld != null && weaponToCreateSecondary.TryGetComp<CompQuality>() is CompQuality compQualityNew2)
                {
                    weaponToCreateSecondary.AllComps.Remove(compQualityNew2);
                    weaponToCreateSecondary.AllComps.Add(compQualityOld);
                }
                if (compInfusedOld != null && weaponToCreateSecondary.AllComps.Find(c => c.GetType().ToString() == "Infused.CompInfused") is ThingComp compInfusedNew2)
                {
                    weaponToCreateSecondary.AllComps.Remove(compInfusedNew2);
                    weaponToCreateSecondary.AllComps.Add(compInfusedOld);
                }
            }

            if (!ConsumeAmmunition()) return;
            Pawn tmpPawn = GetPawn;
            parent.Destroy();
            tmpPawn.equipment.AddEquipment(weaponToCreate);
            if (weaponToCreateSecondary != null) tmpPawn.equipment.AddEquipment(weaponToCreateSecondary);
            PlaySound(Props.transformSound);
        }

        public bool ConsumeAmmunition()
        {
            if (Props.usesAmmunition == null) return true;
            Thing thing = GetPawn.inventory.GetDirectlyHeldThings().ToList().Find(s => s.def == Props.usesAmmunition);
            if (thing != null)
            {
                thing.stackCount = thing.stackCount - 1;
                if (thing.stackCount == 0) thing.Destroy();
                return true;
            }
            Thing thingSecondary = GetPawn.equipment.AllEquipmentListForReading.Find(s => s.def == Props.usesAmmunition);
            if (thingSecondary != null)
            {
                thingSecondary.Destroy();
                return true;
            }
            return false;
        }

        public void MakeTransformJob()
        {
            if (Props.transformTime <= 0f)
            {
                Transform();
            }
            else
            {
                foreach (CompWeaponTransform compWeaponTransform in parent.AllComps.FindAll(c => c is CompWeaponTransform comp1))
                {
                    compWeaponTransform.transformationPending = false;
                }
                transformationPending = true;
                GetPawn.jobs.TryTakeOrderedJob(JobMaker.MakeJob(MHRWDefOf.MHRW_TransformWeapon, GetPawn, parent));
            }
        }

        public float GetTransformTimeInSeconds()
        {
            return Props.transformTime;
        }

        public Texture2D IconTransform
        {
            get
            {
                var resolvedTexture = TexCommand.GatherSpotActive;
                if (!Props.TransformIcon.NullOrEmpty()) resolvedTexture = ContentFinder<Texture2D>.Get(Props.TransformIcon, true);
                return resolvedTexture;
            }
        }

        public void PlaySound(SoundDef soundToPlay)
        {
            if (soundToPlay == null) return;
            SoundInfo info = SoundInfo.InMap(new TargetInfo(GetPawn.PositionHeld, GetPawn.MapHeld, false), MaintenanceType.None);
            soundToPlay?.PlayOneShot(info);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            bool disabled = false;
            string disabledReason = "";
            string defaultLabel = "TransformLabel".Translate().CapitalizeFirst();
            if (!string.IsNullOrEmpty(Props.TransformLabel)) defaultLabel = Props.TransformLabel;
            if (!disabled && GetPawn == null || !GetPawn.equipment.AllEquipmentListForReading.Contains(parent))
            {
                disabled = true;
                disabledReason = "DisabledNotEquipped".Translate(parent.def.label);
            }
            if (!disabled && Props.usesAmmunition != null && GetPawn != null && GetPawn.inventory.GetDirectlyHeldThings().ToList().Find(s => s.def == Props.usesAmmunition) == null && GetPawn.equipment.AllEquipmentListForReading.Find(s => s.def == Props.usesAmmunition) == null)
            {
                disabled = true;
                disabledReason = "DisabledNoAmmunition".Translate(Props.usesAmmunition.label).CapitalizeFirst();
            }
            if (!disabled && Props.needSpecialItemEquipped != null && GetPawn.equipment.AllEquipmentListForReading.Find(e => e.def == Props.needSpecialItemEquipped) == null)
            {
                disabled = true;
                disabledReason = "DisabledNeedsItemEquipped".Translate(Props.needSpecialItemEquipped.label);
            }

            yield return new Command_Action
            {
                action = MakeTransformJob,
                defaultLabel = defaultLabel,
                defaultDesc = Props.TransformDesc,
                icon = IconTransform,
                disabled = disabled,
                disabledReason = disabledReason
            };
            yield break;
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref transformationPending, "transformationPending", false, false);
        }

        public bool transformationPending = false;
    }
}
