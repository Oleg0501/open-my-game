namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockMovementService
    {
        bool TryMove(BlockID blockID, BlockMovementDirection direction, BlockMovementResult result);
    }
}