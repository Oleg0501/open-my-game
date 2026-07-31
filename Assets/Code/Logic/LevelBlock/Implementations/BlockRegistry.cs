using System;
using System.Collections.Generic;
using Code.Logic.LevelBlock.Contracts;

namespace Code.Logic.LevelBlock.Implementations
{
    public class BlockRegistry : IBlockRegistry
    {
        private readonly Dictionary<BlockID, Block> _blocks = new();
        
        public void Register(BlockID blockID, Block block)
        {
            _blocks.Add(blockID, block);
        }

        public void Clear()
        {
            _blocks.Clear();
        }
        
        public Block GetBlock(BlockID blockID)
        {
            return _blocks.TryGetValue(blockID, out var block)
                ? block
                : throw new Exception($"Block with ID '{blockID}' not registered");
        }
    }
}