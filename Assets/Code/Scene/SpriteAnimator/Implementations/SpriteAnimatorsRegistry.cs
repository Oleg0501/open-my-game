using System.Collections.Generic;
using Code.Scene.SpriteAnimator.Contracts;

namespace Code.Scene.SpriteAnimator.Implementations
{
    public class SpriteAnimatorsRegistry : ISpriteAnimatorsRegistry
    {
        private readonly Dictionary<int, SpriteAnimator> _animators = new();
        
        private readonly Dictionary<int, SpriteAnimator> _pendingRegisterAnimators = new();
        private readonly Dictionary<int, SpriteAnimator> _pendingUnregisterAnimators = new();

        private bool _isTicking;
        
        public void Register(int id, SpriteAnimator animator)
        {
            if (_isTicking)
            {
                _pendingRegisterAnimators.Add(id, animator);
                
                return;
            }
            
            _animators.Add(id, animator);
        }
        
        public void Unregister(int id)
        {
            if (_isTicking)
            {
                _pendingUnregisterAnimators.Add(id, _animators[id]);
                
                return;
            }
            
            _animators.Remove(id);
        }

        public void Clear()
        {
            _animators.Clear();
        }
        
        public void Tick(float deltaTime)
        {
            _isTicking = true;
            
            foreach (var animator in _animators.Values)
            {
                animator.Tick(deltaTime);
            }

            _isTicking = false;

            foreach (var pendingRegister in _pendingRegisterAnimators)
            {
                _animators.Add(pendingRegister.Key, pendingRegister.Value);
            }

            foreach (var unPendingRegister in _pendingUnregisterAnimators)
            {
                _animators.Remove(unPendingRegister.Key);
            }
            
            _pendingRegisterAnimators.Clear();
            _pendingUnregisterAnimators.Clear();
        }
    }
}