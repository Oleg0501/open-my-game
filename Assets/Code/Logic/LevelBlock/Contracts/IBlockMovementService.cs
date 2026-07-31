namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockMovementService
    {
        BlockMovementResult Move(BlockID blockID, BlockMovementDirection direction);
    }
}