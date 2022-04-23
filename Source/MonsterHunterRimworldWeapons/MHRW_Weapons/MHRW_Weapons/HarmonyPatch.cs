using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MHRW_Weapons
{
    [StaticConstructorOnStartup]
    static class HarmonyPatch
    {
        static HarmonyPatch()
        {
            var harmony = new Harmony("rimworld.alligator_carnysenpai.mhrw");
            harmony.Patch(AccessTools.Method(typeof(Pawn_EquipmentTracker), "GetGizmos"), null, new HarmonyMethod(typeof(HarmonyPatch).GetMethod("GetGizmos_PostFix")), null); // adds abilities to pawns
        }

        [HarmonyPostfix]
        public static void GetGizmos_PostFix(Pawn_EquipmentTracker __instance, ref IEnumerable<Gizmo> __result) // adds abilities to pawns
        {
            Pawn pawn = __instance.pawn;
            if (!pawn.IsColonist) return;
            if (PawnAttackGizmoUtility.CanShowEquipmentGizmos())
            {
                List<Gizmo> newOutput = new List<Gizmo>();
                newOutput.AddRange(__result);

                if (pawn.Drafted)
                {
                    foreach (ThingWithComps thingWithComps in __instance.AllEquipmentListForReading)
                    {
                        foreach (ThingComp thingComp in thingWithComps.AllComps.FindAll(c => c is CompWeaponTransform comp1))
                        {
                            newOutput.AddRange(thingComp.CompGetGizmosExtra());
                        }
                    }
                }
                __result = newOutput;
            }
        }
    }
}
