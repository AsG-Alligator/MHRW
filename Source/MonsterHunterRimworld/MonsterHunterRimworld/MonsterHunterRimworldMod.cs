using UnityEngine;
using Verse;

namespace MonsterHunterRimworld
{
    public class MonsterHunterRimworldMod : Mod
    {
        MonsterHunterRimworldModSettings monsterHunterRimworldModSettings;

        public MonsterHunterRimworldMod(ModContentPack content) : base(content)
        {
            this.monsterHunterRimworldModSettings = GetSettings<MonsterHunterRimworldModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("ModSettingFirstElderDragonLabel".Translate());
            listingStandard.Gap(listingStandard.verticalSpacing);
            Rect rect1 = listingStandard.GetRect(22f);
            monsterHunterRimworldModSettings.minElderDragonEventDelay = Widgets.HorizontalSlider(rect1, monsterHunterRimworldModSettings.minElderDragonEventDelay, 0f, 120f, false, (monsterHunterRimworldModSettings.minElderDragonEventDelay).ToString("") + "", "0", "120", 1f);

            listingStandard.Gap(listingStandard.verticalSpacing);
            listingStandard.CheckboxLabeled("ModSettingElderDragonWeatherEffectsLabel".Translate(), ref monsterHunterRimworldModSettings.elderDragonWeatherEffects, "ModSettingElderDragonWeatherEffectsTooltip".Translate());

            listingStandard.Gap(listingStandard.verticalSpacing);
            listingStandard.CheckboxLabeled("ModSettingElderDragonScareAnimalsLabel".Translate(), ref monsterHunterRimworldModSettings.elderDragonScareAnimals, "ModSettingElderDragonScareAnimalsTooltip".Translate());

            listingStandard.End();
        }

        public override string SettingsCategory()
        {
            return "Monster Hunter Rimworld";
        }
    }
}
