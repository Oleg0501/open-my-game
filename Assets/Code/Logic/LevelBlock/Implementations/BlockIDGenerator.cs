using Code.Logic.LevelBlock.Contracts;

namespace Code.Logic.LevelBlock.Implementations
{
    public class BlockIDGenerator : IBlockIDGenerator
    {
        private int _index = 0;

        public BlockID Next()
        {
            return new BlockID(_index++);
        }

        public void Reset()
        {
            _index = 0;
        }
    }
}