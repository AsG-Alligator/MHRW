using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using RimWorld.BaseGen;
using Verse;
using Verse.AI.Group;

namespace MonsterHunterRimworld
{
    public class SymbolResolver_SpawnHuntingMonsters : SymbolResolver
    {

        public override void Resolve(ResolveParams rp)
        {
            ResolveParams canReachEdgeParams = rp;
            BaseGen.symbolStack.Push("ensureCanReachMapEdge", canReachEdgeParams);

            CellRect rect = rp.rect;
            //Chance to use the right half of the rect
            if (Rand.Chance(0.5f))
            {
                rp.rect = new CellRect(rect.minX + (rect.Width / 2), rect.minZ, rect.Width / 2, rect.Height);
            }
            else
            {
                rp.rect = new CellRect(rect.minX, rect.minZ, rect.Width / 2, rect.Height);
            }

            IntVec3 spawnLoc = CellFinder.StandableCellNear(BaseGen.globalSettings.map.Center, BaseGen.globalSettings.map, 40);

            PawnKindDef huntingTarget = rp.singlePawnKindDef;

               ResolveParams resolveParams = rp;
                Faction faction = FactionUtility.DefaultFactionFrom(huntingTarget.defaultFactionType);
                Pawn pawnToSpawn = PawnGenerator.GeneratePawn(huntingTarget, faction);

                resolveParams.singlePawnToSpawn = pawnToSpawn;

            GenSpawn.Spawn(pawnToSpawn, spawnLoc, BaseGen.globalSettings.map, Rot4.Random, WipeMode.Vanish, false);

            BaseGen.symbolStack.Push("pawn", resolveParams);
            
        }
    }
}

