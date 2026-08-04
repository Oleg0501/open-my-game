namespace Code.Scene.Block.Contracts
{
    public interface IBlockViewSwipeController
    {
        void BindToBlockSwipeDetection(BlockView blockView);
        void UnsubscribeFromAllBlockSwipes();
    }
}