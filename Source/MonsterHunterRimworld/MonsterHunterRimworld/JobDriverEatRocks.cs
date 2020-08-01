using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MonsterHunterRimworld
{
    public class JobDriverEatRocks : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(TargetA, this.job);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            job.count = 1;

            Toil toilGoto = Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            yield return toilGoto;

            if (this.pawn.TryGetComp<CompDodogama>() is CompDodogama compDodogama)
            {
                this.totalNeededWork = GenTicks.SecondsToTicks(compDodogama.Props.timeToEatInSeconds);
                workLeft = totalNeededWork;
                maxNutritionPercent = compDodogama.Props.maxNutritionFromRock;
            }
            else
            {
                totalNeededWork = -1;
            }

            Toil eatRock = new Toil();
            eatRock.FailOn(() => totalNeededWork < 0).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            eatRock.tickAction = delegate ()
            {
                workLeft--;
                if (workLeft <= 0f)
                {
                    pawn.needs.food.CurLevel += maxNutritionPercent * TargetA.Thing.HitPoints / TargetA.Thing.MaxHitPoints;
                    TargetA.Thing.Destroy();
                    eatRock.actor.jobs.curDriver.ReadyForNextToil();
                }
            };
            eatRock.defaultCompleteMode = ToilCompleteMode.Never;
            eatRock.WithProgressBar(TargetIndex.A, () => 1f - this.workLeft / this.totalNeededWork, false, -0.5f);
            yield return eatRock;
            
            yield break;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref workLeft, "workLeft", 0f, false);
            Scribe_Values.Look<float>(ref totalNeededWork, "totalNeededWork", 0f, false);
            Scribe_Values.Look<float>(ref maxNutritionPercent, "maxNutritionPercent", 0.5f, false);
        }

        private float workLeft;
        private float totalNeededWork;
        private float maxNutritionPercent;
    }
}
