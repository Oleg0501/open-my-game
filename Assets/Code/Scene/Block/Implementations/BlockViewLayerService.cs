using Code.Scene.Contracts;

namespace Code.Scene.Implementations
{
    public class BlockViewLayerService : IBlockViewLayerService
    {
        public int GetLayerFromXY(int x, int y)
        {
            return y * 100 + x;
        }
    }
}