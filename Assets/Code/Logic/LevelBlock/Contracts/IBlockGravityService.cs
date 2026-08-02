namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockGravityService
    {
        void ApplyGravity(BlockMovementResult result);
    }
}