namespace Code.Logic.Blocks.Contracts
{
    public interface IBlockIDGenerator
    {
        BlockID Next();
        void Reset();
    }
}