namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockIDGenerator
    {
        BlockID Next();
        void Reset();
    }
}