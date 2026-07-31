namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockRegistry
    {
        void Register(BlockID blockID, Block block);
        Block GetBlock(BlockID blockID);
        void Clear();
    }
}