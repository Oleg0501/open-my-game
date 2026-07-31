namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockMovementService
    {
        bool TryMove(BlockID blockID, BlockMovementDirection direction, out BlockMovementResult blockMovementResult);
    }
}