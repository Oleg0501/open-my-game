using Code.Logic.Blocks.Contracts;
using Code.Logic.Core;

namespace Code.Logic.Blocks.Implementations
{
    public class BlockRegistry : BaseRegistry<BlockID, Block>, IBlockRegistry
    {
        public void Register(BlockID blockID, Block block)
        {
            RegisterInternal(blockID, block);
        }

        public void Unregister(BlockID blockID)
        {
            UnregisterInternal(blockID);
        }

        public bool Contains(BlockID blockID)
        {
            return ContainsInternal(blockID);
        }

        public void Clear()
        {
            ClearInternal();
        }
        
        public Block GetBlock(BlockID blockID)
        {
            return GetInternal(blockID);
        }
    }
}