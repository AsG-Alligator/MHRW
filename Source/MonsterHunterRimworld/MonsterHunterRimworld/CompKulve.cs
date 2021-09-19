using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;


namespace MonsterHunterRimworld
{
    class CompKulve : ThingComp
    {
        public CompProperties_KulveMantle Props
        {
            get
            {
                return props as CompProperties_KulveMantle;
            }
        }

        

        public override void PostDraw()
        {
            Pawn pawn = this.parent as Pawn;
            if (pawn.health.hediffSet.HasHediff(MHRWDefOf.KulveMantle))
            {
                Material material = Props.graphicData.Graphic.MatAt(pawn.Rotation);
                Vector3 drawLoc = pawn.DrawPos + Props.graphicData.DrawOffsetForRot(pawn.Rotation);
                Mesh mesh = Props.graphicData.Graphic.MeshAt(pawn.Rotation);
                Graphics.DrawMesh(mesh, drawLoc, Quaternion.AngleAxis(0, Vector3.up), material, 0);
            }
            base.PostDraw();
        }

        public override void CompTick()
        {
            base.CompTick();

            Pawn pawn = this.parent as Pawn;
            if (this.addHediffOnce)
            {                
                Hediff mantle = HediffMaker.MakeHediff(MHRWDefOf.KulveMantle, pawn, null);
                pawn.health.AddHediff(mantle);
                this.addHediffOnce = false;   
            }
            if(pawn.health.summaryHealth.SummaryHealthPercent < 0.85f || pawn.Downed || pawn.Dead)
            {
                if (this.removeHediffOnce)
                {
                    pawn.health.hediffSet.hediffs.RemoveAll(x => x.def == MHRWDefOf.KulveMantle);
                    removeHediffOnce = false;
                    var rand = new System.Random();
                    for (int i = 0; i<4; i++)
                    {
                        Thing gold = ThingMaker.MakeThing(ThingDefOf.Gold);
                        gold.stackCount = rand.Next(45, 120);
                        GenSpawn.Spawn(gold, CellFinder.RandomClosewalkCellNear(this.parent.Position, this.parent.Map, 3), pawn.Map);
                    }
                }
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref addHediffOnce, "hediffAdded", true, false);
            Scribe_Values.Look<bool>(ref removeHediffOnce, "hediffAdded", true, false);
        }

        // Token: 0x0400025B RID: 603
        private bool addHediffOnce = true;
        private bool removeHediffOnce = true;
    }

    public class CompProperties_KulveMantle : CompProperties
    {
        public CompProperties_KulveMantle()
        {
            this.compClass = typeof(CompKulve);
        }

        public GraphicData graphicData;

    }

}
