using Code.Logic.Blocks;
using Code.Logic.Core;
using Code.Scene.Block.Contracts;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewsRegistry : BaseRegistry<BlockID, BlockView>, IBlockViewsRegistry
    {
        public BlockView Register(BlockID blockID, BlockView view)
        {
            return RegisterInternal(blockID, view);
        }

        public BlockView Unregister(BlockID blockID)
        {
            return UnregisterInternal(blockID);
        }
        
        public BlockView GetView(BlockID blockID)
        {
            return GetInternal(blockID);
        }

        public BlockView[] GetViewsAll()
        {
            return GetAllInternal();
        }
        
        public void Clear()
        {
            ClearInternal();
        }
    }
}