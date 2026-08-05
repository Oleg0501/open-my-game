namespace Code.Scene.Block.Contracts
{
    public interface IBlockViewSwipeController
    {
        void BindToBlockViewSwipeDetection(BlockView blockView);
        void UnsubscribeFromAllBlockViewSwipes();
    }
}