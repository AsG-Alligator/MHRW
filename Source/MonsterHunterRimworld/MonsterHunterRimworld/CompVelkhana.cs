using RimWorld;
using UnityEngine;
using Verse;

namespace MonsterHunterRimworld
{
    class CompVelkhana : ThingComp
    {
        public override void CompTickRare()
        {
            base.CompTickRare();
            if (!LoadedModManager.GetMod<MonsterHunterRimworldMod>().GetSettings<MonsterHunterRimworldModSettings>().elderDragonWeatherEffects) return;
            if (!(parent is Pawn elderDragon)) return;
            if (!WyvernUtility.IsElderDragon(elderDragon)) return;
            if (elderDragon.Dead) return;
            Map map = elderDragon.Map;
            if (map == null) return;

            GameConditionManager gameConditionManager = map.GameConditionManager;
            if (gameConditionManager.ConditionIsActive(GameConditionDefOf.ColdSnap)) return;
            int duration = Mathf.RoundToInt(0.5f * 60000f); // lasts half a day after elder dragon dies
            GameCondition cond = GameConditionMaker.MakeCondition(GameConditionDefOf.ColdSnap, duration);
            gameConditionManager.RegisterCondition(cond);
        }
    }
}