using System;

namespace Code.Logic.LevelBlock
{
    [Serializable]
    public class Block
    {
        public BlockID ID { get; private set; }
        public string ConfigID { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        public Block(BlockID id, string configId, int x, int y)
        {
            ID = id;
            ConfigID = configId;
            X = x;
            Y = y;
        }
    }
}