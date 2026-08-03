using System.Collections.Generic;

namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockMatchDestroyer
    {
        void DestroyBlocks(HashSet<Block> blocks);
    }
}