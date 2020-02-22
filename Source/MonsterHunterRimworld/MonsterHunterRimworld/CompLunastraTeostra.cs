﻿using RimWorld;
using UnityEngine;
using Verse;

namespace MonsterHunterRimworld
{
    class CompLunastraTeostra : ThingComp
    {
        public override void CompTickRare()
        {
            base.CompTickRare();
            if (!(parent is Pawn elderDragon)) return;
            if (!WyvernUtility.IsElderDragon(elderDragon)) return;
            if (elderDragon.health.Dead) return;
            Map map = elderDragon.Map;
            if (map == null) return;

            GameConditionManager gameConditionManager = map.GameConditionManager;
            if (elderDragon.gender == Gender.Male)
            {
                if (gameConditionManager.ConditionIsActive(GameConditionDefOf.HeatWave)) return;
                int duration = Mathf.RoundToInt(0.5f * 60000f); // lasts half a day after elder dragon dies
                GameCondition cond = GameConditionMaker.MakeCondition(GameConditionDefOf.HeatWave, duration, 0);
                gameConditionManager.RegisterCondition(cond);
            }
            else
            {
                if (gameConditionManager.ConditionIsActive(GameConditionDefOf.Eclipse)) return;
                int duration = Mathf.RoundToInt(0.5f * 60000f); // lasts half a day after elder dragon dies
                GameCondition cond = GameConditionMaker.MakeCondition(GameConditionDefOf.Eclipse, duration, 0);
                gameConditionManager.RegisterCondition(cond);
            }
        }
    }
}
