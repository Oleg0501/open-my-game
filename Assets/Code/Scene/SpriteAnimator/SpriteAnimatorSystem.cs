using Code.Scene.Contracts;
using Code.Scene.SpriteAnimator.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.SpriteAnimator
{
    public class SpriteAnimatorSystem : MonoBehaviour
    {
        private ISpriteAnimatorsRegistry _spriteAnimatorsRegistry;

        [Inject]
        private void Construct(ISpriteAnimatorsRegistry spriteAnimatorsRegistry)
        {
            _spriteAnimatorsRegistry = spriteAnimatorsRegistry;
        }
        
        private void Update()
        {
            _spriteAnimatorsRegistry.Tick(Time.deltaTime);
        }
    }
}