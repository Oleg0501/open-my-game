using System.Collections.Generic;
using Code.Logic.Core;
using Code.Scene.Core.Contracts;

namespace Code.Scene.Core.Implementations
{
    public class TickableRegistry : BaseRegistry<int, ITickable>, ITickableRegistry
    {
        private readonly Dictionary<int, ITickable> _pendingRegisterTickables = new();
        private readonly Dictionary<int, ITickable> _pendingUnregisterTickables = new();

        private bool _isTicking;
        
        public void Register(int id, ITickable tickable)
        {
            if (_isTicking)
            {
                _pendingRegisterTickables.Add(id, tickable);
                
                return;
            }
            
            RegisterInternal(id, tickable);
        }
        
        public void Unregister(int id)
        {
            if (_isTicking)
            {
                _pendingUnregisterTickables.Add(id, GetInternal(id));
                
                return;
            }
            
            UnregisterInternal(id);
        }

        public void Clear()
        {
            ClearInternal();
        }
        
        public void Tick(float deltaTime)
        {
            _isTicking = true;

            var tickables = GetAllInternal();
            
            foreach (var tickable in tickables)
            {
                tickable.Tick(deltaTime);
            }

            _isTicking = false;

            foreach (var pendingRegister in _pendingRegisterTickables)
            {
                RegisterInternal(pendingRegister.Key, pendingRegister.Value);
            }

            foreach (var unPendingRegister in _pendingUnregisterTickables)
            {
                UnregisterInternal(unPendingRegister.Key);
            }
            
            _pendingRegisterTickables.Clear();
            _pendingUnregisterTickables.Clear();
        }
    }
}