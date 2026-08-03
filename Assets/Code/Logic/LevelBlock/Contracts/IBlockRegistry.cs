namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockRegistry
    {
        void Register(BlockID blockID, Block block);
        void Unregister(BlockID blockID);
        bool Contains(BlockID blockID);
        Block GetBlock(BlockID blockID);
        void Clear();
    }
}