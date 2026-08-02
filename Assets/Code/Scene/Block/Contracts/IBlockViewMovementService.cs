using System.Threading.Tasks;
using Code.Logic.LevelBlock;

namespace Code.Scene.Block.Contracts
{
    public interface IBlockViewMovementService
    {
        Task MoveAsync(BlockMovementResult blockMovementResult);
    }
}