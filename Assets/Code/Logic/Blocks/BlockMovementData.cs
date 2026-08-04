using System.Collections.Generic;
using UnityEngine;

namespace Code.Logic.Blocks
{
    public class BlockMovementData
    {
        public readonly List<BlockMovementArgs> Movements = new();

        public void Add(BlockID id, Vector2Int fromPoint, Vector2Int toPoint)
        {
            var info = new BlockMovementArgs { BlockID = id, FromPoint = fromPoint, ToPoint = toPoint };
            
            Movements.Add(info);
        }
    }
}