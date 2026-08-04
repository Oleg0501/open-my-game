using System.Collections.Generic;

namespace Code.Logic.Blocks.Contracts
{
    public interface IBlockMatchDestroyer
    {
        void DestroyBlocks(IReadOnlyCollection<Block> blocks);
    }
}