using System.Threading.Tasks;
using Code.Logic.Blocks;

namespace Code.Scene.Block.Contracts
{
    public interface IBlockViewMovementService
    {
        Task MoveAsync(BlockMovementData data);
    }
}