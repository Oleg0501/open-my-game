using System.Threading;
using Code.Scene.Core.Contracts;

namespace Code.Scene.Core.Implementations
{
    public class CancellationTokenService : ICancellationTokenService
    {
        public CancellationToken Token => _source.Token;
        
        private CancellationTokenSource _source = new();
        
        public void Cancel()
        {
            if (!_source.IsCancellationRequested)
            {
                _source.Cancel();
            }
        }

        public void Reset()
        {
            _source.Dispose();
            _source = new CancellationTokenSource();
        }
    }
}