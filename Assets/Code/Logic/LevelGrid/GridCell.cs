using System;
using Code.Logic.LevelBlock;

namespace Code.Logic.LevelGrid
{
    [Serializable]
    public class GridCell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Block Block { get; set; }
        public bool IsEmpty => Block == null;

        public GridCell(int x, int y, Block block = null)
        {
            X = x;
            Y = y;
            Block = block;
        }
    }
}