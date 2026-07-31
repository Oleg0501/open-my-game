using Code.Logic.LevelBlock;

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