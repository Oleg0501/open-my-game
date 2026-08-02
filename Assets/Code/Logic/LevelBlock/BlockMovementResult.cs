using System.Collections.Generic;
using UnityEngine;

namespace Code.Logic.LevelBlock
{
    public class BlockMovementResult
    {
        public readonly List<BlockMovementInfo> Moves = new();

        public void Add(BlockID id, Vector2Int from, Vector2Int to)
        {
            Moves.Add(new BlockMovementInfo { BlockID = id, From = from, To = to });
        }
    }
}