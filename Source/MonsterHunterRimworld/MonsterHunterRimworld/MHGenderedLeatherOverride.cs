using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;

namespace MonsterHunterRimworld
{
    class MHGenderedLeatherOverride : Pawn
    {

		public override IEnumerable<Thing> ButcherProducts(Pawn butcher, float efficiency)
        {
			if (RaceProps.meatDef != null)
			{
				int num = GenMath.RoundRandom(this.GetStatValue(StatDefOf.MeatAmount) * efficiency);
				if (num > 0)
				{
					Thing thing = ThingMaker.MakeThing(RaceProps.meatDef);
					thing.stackCount = num;
					yield return thing;
				}
			}
			


			if (this.def.defName == "Rath")
			{
				
				if (this.gender == Gender.Male)
				{
					int num2 = GenMath.RoundRandom(this.GetStatValue(StatDefOf.LeatherAmount) * efficiency);
					if (num2 > 0)
					{
						Thing thing2 = ThingMaker.MakeThing(MHRWDefOf.Leather_Rathalos);
						thing2.stackCount = num2;
						yield return thing2;
					}
				}
				else
				{
					int num2 = GenMath.RoundRandom(this.GetStatValue(StatDefOf.LeatherAmount) * efficiency);
					if (num2 > 0)
					{
						Thing thing2 = ThingMaker.MakeThing(MHRWDefOf.Leather_Rathian);
						thing2.stackCount = num2;
						yield return thing2;
					}
				}
			}
			if (this.def.defName == "Tesuka")
			{
				if (this.gender == Gender.Male)
				{
					int num2 = GenMath.RoundRandom(this.GetStatValue(StatDefOf.LeatherAmount) * efficiency);
					if (num2 > 0)
					{
						Thing thing2 = ThingMaker.MakeThing(MHRWDefOf.Leather_Teostra);
						thing2.stackCount = num2;
						yield return thing2;
					}
				}
				else
				{
					int num2 = GenMath.RoundRandom(this.GetStatValue(StatDefOf.LeatherAmount) * efficiency);
					if (num2 > 0)
					{
						Thing thing2 = ThingMaker.MakeThing(MHRWDefOf.Leather_Lunastra);
						thing2.stackCount = num2;
						yield return thing2;
					}
				}
			}

			if (RaceProps.Humanlike)
			{
				yield break;
			}
			PawnKindLifeStage lifeStage = ageTracker.CurKindLifeStage;
			if (lifeStage.butcherBodyPart == null || (gender != 0 && (gender != Gender.Male || !lifeStage.butcherBodyPart.allowMale) && (gender != Gender.Female || !lifeStage.butcherBodyPart.allowFemale)))
			{
				yield break;
			}
			while (true)
			{
				BodyPartRecord bodyPartRecord = (from x in health.hediffSet.GetNotMissingParts()
												 where x.IsInGroup(lifeStage.butcherBodyPart.bodyPartGroup)
												 select x).FirstOrDefault();
				if (bodyPartRecord != null)
				{
					health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, this, bodyPartRecord));
					yield return (lifeStage.butcherBodyPart.thing == null) ? ThingMaker.MakeThing(bodyPartRecord.def.spawnThingOnRemoved) : ThingMaker.MakeThing(lifeStage.butcherBodyPart.thing);
					continue;
				}
				break;
			}
		}
    }

}
