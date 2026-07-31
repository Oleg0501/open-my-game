using Code.Logic.LevelBlock;

namespace Code.Scene.Contracts
{
    public interface IBlockViewSwipeController
    {
        void SubscribeOnBlockSwipe(BlockID blockID);
        void UnsubscribeFromAllBlockSwipes();
    }
}