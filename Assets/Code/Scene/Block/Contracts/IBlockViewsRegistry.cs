using Code.Logic.Blocks;

namespace Code.Scene.Block.Contracts
{
    public interface IBlockViewsRegistry
    {
        BlockView Register(BlockID blockID, BlockView view);
        BlockView Unregister(BlockID blockID);
        BlockView GetView(BlockID blockID);
        BlockView[] GetViewsAll();
        void Clear();
    }
}