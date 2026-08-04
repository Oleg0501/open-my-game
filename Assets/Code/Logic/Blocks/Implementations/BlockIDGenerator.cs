using Code.Logic.Blocks.Contracts;

namespace Code.Logic.Blocks.Implementations
{
    public class BlockIDGenerator : IBlockIDGenerator
    {
        private int _index;

        public BlockID Next()
        {
            var blockID = new BlockID(_index++);
            
            return blockID;
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}