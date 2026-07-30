using Code.Logic.LevelGrid;

namespace Code.Logic.LevelBlock
{
    public class BlockMovementResult
    {
        public GridCell FromCell;
        public GridCell ToCell;

        public Block FirstBlock;
        public Block SecondBlock;

        public bool IsSwap;
    }
}