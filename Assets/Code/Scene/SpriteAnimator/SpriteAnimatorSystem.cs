using Code.Scene.Core.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.SpriteAnimator
{
    public class SpriteAnimatorSystem : MonoBehaviour
    {
        private ITickableRegistry _tickableRegistry;

        [Inject]
        private void Construct(ITickableRegistry tickableRegistry)
        {
            _tickableRegistry = tickableRegistry;
        }
        
        private void Update()
        {
            _tickableRegistry.Tick(Time.deltaTime);
        }
    }
}