using System.Collections.Generic;
using Code.Scene.Core.Contracts;

namespace Code.Scene.Core.Implementations
{
    public class TickableRegistry : ITickableRegistry
    {
        private readonly Dictionary<int, ITickable> _tickables = new();
        
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
            
            _tickables.Add(id, tickable);
        }
        
        public void Unregister(int id)
        {
            if (_isTicking)
            {
                _pendingUnregisterTickables.Add(id, _tickables[id]);
                
                return;
            }
            
            _tickables.Remove(id);
        }

        public void Clear()
        {
            _tickables.Clear();
        }
        
        public void Tick(float deltaTime)
        {
            _isTicking = true;
            
            foreach (var tickable in _tickables.Values)
            {
                tickable.Tick(deltaTime);
            }

            _isTicking = false;

            foreach (var pendingRegister in _pendingRegisterTickables)
            {
                _tickables.Add(pendingRegister.Key, pendingRegister.Value);
            }

            foreach (var unPendingRegister in _pendingUnregisterTickables)
            {
                _tickables.Remove(unPendingRegister.Key);
            }
            
            _pendingRegisterTickables.Clear();
            _pendingUnregisterTickables.Clear();
        }
    }
}