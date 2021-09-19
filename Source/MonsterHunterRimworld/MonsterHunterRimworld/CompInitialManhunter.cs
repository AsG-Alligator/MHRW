using System;
using Verse;
using RimWorld;

namespace MonsterHunterRimworld
{
	// Token: 0x020000C6 RID: 198
	public class CompInitialManhunter : ThingComp
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00017D45 File Offset: 0x00015F45
		// Token: 0x0600031C RID: 796 RVA: 0x00017D6C File Offset: 0x00015F6C
		public override void CompTick()
		{
			base.CompTickRare();
			if (this.addManhunterOnce)
			{
				(this.parent as Pawn).mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
				this.addManhunterOnce = false;
			}
		}

		// Token: 0x0400025B RID: 603
		private bool addManhunterOnce = true;
	}

	public class CompProperties_CompInitialManhunter : CompProperties
	{
		public CompProperties_CompInitialManhunter()
		{
			this.compClass = typeof(CompInitialManhunter);
		}
	}
}