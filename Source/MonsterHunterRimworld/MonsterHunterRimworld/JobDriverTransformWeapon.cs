using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MonsterHunterRimworld
{
    public class JobDriverTransformWeapon : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            job.count = 1;

            Toil transformWeaponPreparations = new Toil();
            transformWeaponPreparations.FailOn(() => totalNeededWork < 0);
            transformWeaponPreparations.initAction = delegate ()
            {
                GetActor().pather.StopDead();
                ThingWithComps thingToTransform = (ThingWithComps)TargetB;
                if (thingToTransform != null && thingToTransform.AllComps.Find(c => c is CompWeaponTransform comp1 && comp1.transformationPending) is CompWeaponTransform compWeaponTransform)
                {
                    totalNeededWork = GenTicks.SecondsToTicks(compWeaponTransform.GetTransformTimeInSeconds());
                    workLeft = totalNeededWork;
                }
                else
                {
                    totalNeededWork = -1;
                }
            };
            transformWeaponPreparations.tickAction = delegate ()
            {
                workLeft--;
                if (workLeft <= 0f)
                {
                    transformWeaponPreparations.actor.jobs.curDriver.ReadyForNextToil();
                }
            };
            transformWeaponPreparations.defaultCompleteMode = ToilCompleteMode.Never;
            transformWeaponPreparations.WithProgressBar(TargetIndex.A, () => 1f - this.workLeft / this.totalNeededWork, false, -0.5f);
            yield return transformWeaponPreparations;

            Toil transformWeapon = new Toil();
            transformWeapon.FailOn(() => totalNeededWork < 0);
            transformWeapon.initAction = delegate ()
            {
                ThingWithComps thingToTransform = (ThingWithComps)TargetB;
                if (thingToTransform != null && thingToTransform.AllComps.Find(c => c is CompWeaponTransform comp1 && comp1.transformationPending) is CompWeaponTransform compWeaponTransform)
                {
                    compWeaponTransform.Transform();
                    compWeaponTransform.transformationPending = false;
                }
            };
            yield return transformWeapon;

            yield break;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref workLeft, "workLeft", 0f, false);
            Scribe_Values.Look<float>(ref totalNeededWork, "totalNeededWork", 0f, false);
        }

        private float workLeft;
        private float totalNeededWork;
    }
}
