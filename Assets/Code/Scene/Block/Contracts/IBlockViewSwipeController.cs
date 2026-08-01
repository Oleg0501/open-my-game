using Code.Scene.Block;

namespace Code.Scene.Contracts
{
    public interface IBlockViewSwipeController
    {
        void BindToBlockSwipeDetection(BlockView blockView);
        void UnsubscribeFromAllBlockSwipes();
    }
}