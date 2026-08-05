using System.Threading;

namespace Code.Scene.Core.Contracts
{
    public interface ICancellationTokenService
    {
        CancellationToken Token { get; }
        
        void Cancel();
        void Reset();
    }
}