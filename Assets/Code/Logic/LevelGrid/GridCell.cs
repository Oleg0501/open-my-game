using System;

namespace Code.Logic.LevelGrid
{
    [Serializable]
    public class GridCell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public LevelBlock.Block Block { get; set; }

        public GridCell(int x, int y, LevelBlock.Block block = null)
        {
            X = x;
            Y = y;
            Block = block;
        }
    }
}