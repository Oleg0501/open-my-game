using Code.Scene.Block.Contracts;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewLayerService : IBlockViewLayerService
    {
        public int GetLayerFromXY(int x, int y)
        {
            return y * 100 + x;
        }
    }
}