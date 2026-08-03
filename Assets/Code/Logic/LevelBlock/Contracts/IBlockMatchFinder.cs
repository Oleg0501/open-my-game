using System.Collections.Generic;

namespace Code.Logic.LevelBlock.Contracts
{
    public interface IBlockMatchFinder
    {
        HashSet<Block> FindMatches();
    }
}