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
    public class CompProperties_HuntingSong : CompProperties
    {
        public CompProperties_HuntingSong()
        {
            compClass = typeof(CompHuntingSong);
        }

        // Gizmo
        public string abilityLabel; // the label on the ability gizmo
        public string abilityDesc; // the description of the ability gizmo
        public string abilityIcon; // the icon of the ability gizmo
        public float abilityIconAngle = 0f; // clockwise rotation of the icon
        public Vector2 abilityIconOffset = new Vector2(0f, 0f); // moves the icon left/right/up/down
        public Color? abilityColor; // color of the label default: none, format (r, g, b) with r, g or b being between 0 and 1
        public KeyBindingDef hotKey; // if you want to assign a hotkey
        public bool displayGizmoWhileUndrafted = true; // if not displayed the ai also cannot use it in the undrafted state
        public bool displayGizmoWhileDrafted = true; // if not displayed the ai also cannot use it in the drafted state
    }
}
