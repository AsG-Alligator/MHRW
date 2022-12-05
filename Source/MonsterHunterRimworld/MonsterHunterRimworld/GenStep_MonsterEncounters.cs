using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using RimWorld.BaseGen;
using Verse;

namespace MonsterHunterRimworld
{
    public class GenStep_BasicGenMHRW : GenStep
    {

        protected Dictionary<string, float> randomRoomEvents = new Dictionary<string, float>();

        protected CellRect adventureRegion;

        protected ResolveParams baseResolveParams;

        public override int SeedPart => 44867541;

        public override void Generate(Map map, GenStepParams parms)
        {
            int num = map.Size.x / 10;
            int num2 = 8 * map.Size.x / 10;
            int num3 = map.Size.z / 10;
            int num4 = 8 * map.Size.z / 10;
            this.adventureRegion = new CellRect(num, num3, num2, num4);
            this.adventureRegion.ClipInsideMap(map);
            BaseGen.globalSettings.map = map;
            this.randomRoomEvents.Clear();
            IntVec3 playerStartSpot;
            CellFinder.TryFindRandomEdgeCellWith((IntVec3 v) => GenGrid.Standable(v, map), map, 0f, out playerStartSpot);
            MapGenerator.PlayerStartSpot = playerStartSpot;
            this.baseResolveParams = default(ResolveParams);
            foreach (string current in this.randomRoomEvents.Keys)
            {
                this.baseResolveParams.SetCustom<float>(current, this.randomRoomEvents[current], false);
            }
        }
    }

    public class GenStep_MonsterEncounter_Rathalos : GenStep_BasicGenMHRW
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = PawnKindDef.Named("Rath");
            //Pawn pawn = PawnGenerator.GeneratePawn(MHRWDefOf.Rath, null);
            //pawn.gender = Gender.Male;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            //animalResolveParams.singlePawnToSpawn = pawn;
            animalResolveParams.faction = FactionUtility.DefaultFactionFrom(huntingKind.defaultFactionType);
            BaseGen.symbolStack.Push("MHRW_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }
}
