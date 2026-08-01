using System.Collections.Generic;
using Code.Scene.Contracts;
using Code.Scene.SpriteAnimator.Contracts;

namespace Code.Scene.SpriteAnimator.Implementations
{
    public class SpriteAnimatorsRegistry : ISpriteAnimatorsRegistry
    {
        private readonly Dictionary<int, Scene.SpriteAnimator.SpriteAnimator> _animators = new();
        
        public void Register(int id, Scene.SpriteAnimator.SpriteAnimator animator)
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