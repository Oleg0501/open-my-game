using System.Collections.Generic;

namespace Code.Logic.LevelBlock.Implementations
{
    public class BlockMatchResult
    {
        public IReadOnlyList<Block> Blocks { get; }

        public BlockMatchResult(IReadOnlyList<Block> blocks)
        {
            Blocks = blocks;
        }
    }
}