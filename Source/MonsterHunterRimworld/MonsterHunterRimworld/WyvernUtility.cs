using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                switch (Rand.RangeInclusive(1, 4))
                {
                    case 1:
                        return MHRWDefOf.KushalaDaora;
                    case 2:
                        return MHRWDefOf.Tesuka;
                    case 3:
                        return MHRWDefOf.Tesuka; // 2 times because different gender is relevant
                    case 4:
                        return MHRWDefOf.Nergigante;
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
