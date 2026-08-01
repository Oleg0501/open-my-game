using Code.Logic.LevelBlock;
using Code.Scene.Block;

namespace Code.Scene.Contracts
{
    public interface IBlockViewsRegistry
    {
        void Register(BlockID blockID, BlockView view);
        void Clear();
        BlockView GetView(BlockID blockID);
        BlockView[] GetViewsAll();
    }
}