using System.Collections.Generic;

namespace Code.Logic.Blocks.Contracts
{
    public interface IBlockMatchFinder
    {
        IReadOnlyCollection<Block> FindMatches();
    }
}