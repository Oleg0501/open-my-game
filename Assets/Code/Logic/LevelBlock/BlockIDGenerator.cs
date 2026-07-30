namespace Code.Logic.LevelBlock
{
    public class BlockIDGenerator
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