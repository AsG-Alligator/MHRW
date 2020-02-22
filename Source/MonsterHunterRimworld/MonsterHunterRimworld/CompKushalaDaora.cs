using Verse;

namespace MonsterHunterRimworld
{
    public class CompKushalaDaora : ThingComp
    {
        public override void CompTickRare()
        {
            base.CompTickRare();
            if (!(parent is Pawn elderDragon)) return;
            if (!WyvernUtility.IsElderDragon(elderDragon)) return;
            if (elderDragon.health.Dead) return;
            Map map = elderDragon.Map;
            if (map == null) return;
            WeatherDef newWeather = new WeatherDef();
            newWeather = WeatherDef.Named("RainyThunderstorm");
            if (map.weatherManager.curWeather != newWeather)
            {
                map.weatherManager.TransitionTo(newWeather);
            }
        }
    }
}
