using System.Collections.Generic;
using Code.Scene.Contracts;

namespace Code.Scene.Implementations
{
    public class SpriteAnimatorsRegistry : ISpriteAnimatorsRegistry
    {
        private readonly Dictionary<int, SpriteAnimator> _animators = new();
        
        public void Register(int id, SpriteAnimator animator)
        {
            _animators.Add(id, animator);
        }
        
        public void Unregister(int id)
        {
            _animators.Remove(id);
        }

        public void Clear()
        {
            _animators.Clear();
        }
        
        public void Tick(float deltaTime)
        {
            foreach (var animator in _animators.Values)
            {
                animator.Tick(deltaTime);
            }
        }
    }
}