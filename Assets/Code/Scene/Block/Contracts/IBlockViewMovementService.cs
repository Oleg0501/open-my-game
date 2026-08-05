using System.Threading;
using Code.Logic.Blocks;
using Cysharp.Threading.Tasks;

namespace Code.Scene.Block.Contracts
{
    public interface IBlockViewMovementService
    {
        UniTask MoveAsync(BlockMovementData data, CancellationToken cancellationToken);
    }
}