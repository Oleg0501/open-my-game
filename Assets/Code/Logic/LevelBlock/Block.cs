using System;

namespace Code.Logic.LevelBlock
{
    [Serializable]
    public class Block
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BlockConfig BlockConfig { get; private set; }

        public Block(int x, int y, BlockConfig blockConfig)
        {
            X = x;
            Y = y;
            BlockConfig = blockConfig;
        }
    }
}