using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace MonsterHunterRimworld
{
    public static class WyvernUtility
    {
        public static PawnKindDef GetSmallMonsterGroupForLargeMonster(PawnKindDef pawnKindDef)
        {
            if (pawnKindDef == MHRWDefOf.GreatJagras) return MHRWDefOf.Jagras;
            if (pawnKindDef == MHRWDefOf.GreatGirros) return MHRWDefOf.Girros;
            return null;
        }

        public static bool IsElderDragon(PawnKindDef pawnKindDef)
        {
            if (pawnKindDef == MHRWDefOf.KushalaDaora) return true;
            if (pawnKindDef == MHRWDefOf.Tesuka) return true;
            if (pawnKindDef == MHRWDefOf.Nergigante) return true;
            return false;
        }

        public static bool IsElderDragon(Pawn pawn)
        {
            return IsElderDragon(pawn.RaceProps.AnyPawnKind);
        }

        public static bool IsSameSpecies(Pawn pawn1, Pawn pawn2)
        {
            return pawn1.RaceProps.AnyPawnKind == pawn2.RaceProps.AnyPawnKind;
        }

        public static PawnKindDef GetRandomLargeMonsterForEvent(Map map)
        {
            List<PawnKindDef> possibleLargeMonster = GetLargeWyvernDefList().ToList().FindAll(w => w.race.statBases.GetStatValueFromList(StatDefOf.ComfyTemperatureMin, 0f) < map.mapTemperature.OutdoorTemp && w.race.statBases.GetStatValueFromList(StatDefOf.ComfyTemperatureMax, 0f) > map.mapTemperature.OutdoorTemp);
            if (map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout))
            {
                possibleLargeMonster.RemoveAll(p => p.race.statBases.GetStatValueFromList(StatDefOf.ToxicSensitivity, 1f) > 0f);
            }
            if (possibleLargeMonster.Count > 0) return possibleLargeMonster.RandomElement();
            return null;
        }

        public static IEnumerable<PawnKindDef> GetLargeWyvernDefList()
        {
            yield return MHRWDefOf.KuluYaKu;
            yield return MHRWDefOf.Genprey;
            yield return MHRWDefOf.Giaprey;
            yield return MHRWDefOf.TzitziYaKu;
            yield return MHRWDefOf.PukeiPukei;
            yield return MHRWDefOf.Barroth;
            yield return MHRWDefOf.Deviljho;
            yield return MHRWDefOf.Jyuratodus;
            yield return MHRWDefOf.Lavasioth;
            yield return MHRWDefOf.Anjanath;
            yield return MHRWDefOf.TobiKadachi;
            yield return MHRWDefOf.GreatJagras;
            yield return MHRWDefOf.GreatGirros;
            yield return MHRWDefOf.Dodogama;
            yield return MHRWDefOf.Odogaron;
            yield return MHRWDefOf.Rath;
            yield return MHRWDefOf.Rath; // 2 times because different gender is relevant
            yield return MHRWDefOf.Bazelgeuse;
            yield return MHRWDefOf.Legiana;
            yield return MHRWDefOf.Diablos;
            yield return MHRWDefOf.Paolumu;
            yield break;
        }

        public static PawnKindDef GetRandomSmallMonsterForEvent(Map map)
        {
            List<PawnKindDef> possibleSmallMonster = GetSmallMonsterList().ToList().FindAll(w => w.race.statBases.GetStatValueFromList(StatDefOf.ComfyTemperatureMin, 0f) < map.mapTemperature.OutdoorTemp && w.race.statBases.GetStatValueFromList(StatDefOf.ComfyTemperatureMax, 0f) > map.mapTemperature.OutdoorTemp);
            if (map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout))
            {
                possibleSmallMonster.RemoveAll(p => p.race.statBases.GetStatValueFromList(StatDefOf.ToxicSensitivity, 1f) > 0f);
            }
            if (possibleSmallMonster.Count > 0) return possibleSmallMonster.RandomElement();
            return null;
        }

        public static IEnumerable<PawnKindDef> GetSmallMonsterList()
        {
            yield return MHRWDefOf.Jagras;
            yield return MHRWDefOf.Girros;
            yield return MHRWDefOf.Aptonoth;
            yield break;
        }

        public static PawnKindDef GetRandomElderDragonForEvent(Map map)
        {
            List<PawnKindDef> possibleElderDragon = GetElderDragonList().ToList();
            if (map.gameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout))
            {
                possibleElderDragon.RemoveAll(p => p.race.statBases.GetStatValueFromList(StatDefOf.ToxicSensitivity, 1f) > 0f);
            }
            if (possibleElderDragon.Count > 0) return possibleElderDragon.RandomElement();
            return null;
        }

        public static IEnumerable<PawnKindDef> GetElderDragonList()
        {
            yield return MHRWDefOf.KushalaDaora;
            yield return MHRWDefOf.Tesuka;
            yield return MHRWDefOf.Tesuka; // 2 times because different gender is relevant
            yield return MHRWDefOf.Nergigante;
            yield return MHRWDefOf.Kirin;
            yield break;
        }

        public static PawnKindDef GetRandomPawnKindForWyvernType(string wyvernType)
        {
            wyvernType = wyvernType.Replace(" ", "").ToUpper();
            if (wyvernType.Equals("BIRDWYVERN"))
            {
                switch (Rand.RangeInclusive(1, 5))
                {
                    case 1:
                        return MHRWDefOf.KuluYaKu;
                    case 2:
                        return MHRWDefOf.Genprey;
                    case 3:
                        return MHRWDefOf.Giaprey;
                    case 4:
                        return MHRWDefOf.TzitziYaKu;
                    case 5:
                        return MHRWDefOf.PukeiPukei;
                }
            }
            else if (wyvernType.Equals("BRUTEWYVERN"))
            {
                switch (Rand.RangeInclusive(1, 5))
                {
                    case 1:
                        return MHRWDefOf.Barroth;
                    case 2:
                        return MHRWDefOf.Deviljho;
                    case 3:
                        return MHRWDefOf.Jyuratodus;
                    case 4:
                        return MHRWDefOf.Lavasioth;
                    case 5:
                        return MHRWDefOf.Anjanath;
                }
            }
            else if (wyvernType.Equals("ELDERDRAGON"))
            {
                switch (Rand.RangeInclusive(1, 5))
                {
                    case 1:
                        return MHRWDefOf.KushalaDaora;
                    case 2:
                        return MHRWDefOf.Tesuka;
                    case 3:
                        return MHRWDefOf.Tesuka; // 2 times because different gender is relevant
                    case 4:
                        return MHRWDefOf.Nergigante;
                    case 5:
                        return MHRWDefOf.Kirin;
                }
            }
            else if (wyvernType.Equals("FANGEDWYVERN"))
            {
                switch (Rand.RangeInclusive(1, 5))
                {
                    case 1:
                        return MHRWDefOf.TobiKadachi;
                    case 2:
                        return MHRWDefOf.GreatJagras;
                    case 3:
                        return MHRWDefOf.GreatGirros;
                    case 4:
                        return MHRWDefOf.Dodogama;
                    case 5:
                        return MHRWDefOf.Odogaron;
                }
            }
            else if (wyvernType.Equals("FYLINGWYVERN"))
            {
                switch (Rand.RangeInclusive(1, 6))
                {
                    case 1:
                        return MHRWDefOf.Rath;
                    case 2:
                        return MHRWDefOf.Rath; // 2 times because different gender is relevant
                    case 3:
                        return MHRWDefOf.Bazelgeuse;
                    case 4:
                        return MHRWDefOf.Legiana;
                    case 5:
                        return MHRWDefOf.Diablos;
                    case 6:
                        return MHRWDefOf.Paolumu;
                }
            }
            else if (wyvernType.Equals("SMALLMONSTER"))
            {
                switch (Rand.RangeInclusive(1, 3))
                {
                    case 1:
                        return MHRWDefOf.Jagras;
                    case 2:
                        return MHRWDefOf.Girros;
                    case 3:
                        return MHRWDefOf.Aptonoth;
                }
            }
            else
            {
                return MHRWDefOf.Aptonoth;
            }
            return MHRWDefOf.Aptonoth;
        }
    }
}
