namespace Code.Logic.Blocks.Contracts
{
    public interface IBlockMovementService
    {
        bool TryMove(BlockID blockID, BlockMovementDirection direction, BlockMovementData data);
    }
}