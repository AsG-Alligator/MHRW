using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using UnityEngine;

namespace MonsterHunterRimworld
{
    class MHRenderWhileAiming : ThingComp
    {
        public CompProperties_MHRenderWhileAiming Props
        {
            get
            {
                return props as CompProperties_MHRenderWhileAiming;
            }
        }
        public override void PostDraw()
        {
            Pawn pawn = this.parent as Pawn;
            if (pawn.stances.curStance is Stance_Busy stance_Busy && !stance_Busy.neverAimWeapon && stance_Busy.focusTarg.IsValid && pawn.ageTracker.Adult)
            {
                Material material = Props.graphicData.Graphic.MatAt(pawn.Rotation);
                Vector3 drawLoc = pawn.DrawPos + Props.graphicData.DrawOffsetForRot(pawn.Rotation);
                Mesh mesh = Props.graphicData.Graphic.MeshAt(pawn.Rotation);
                Graphics.DrawMesh(mesh, drawLoc, Quaternion.AngleAxis(0, Vector3.up), material, 0);
            }
            base.PostDraw();
        }



    }

    public class CompProperties_MHRenderWhileAiming : CompProperties
    {
        public CompProperties_MHRenderWhileAiming()
        {
            this.compClass = typeof(MHRenderWhileAiming);
        }

        public GraphicData graphicData;

    }
}
