using UnityEngine;

namespace Code.Scene.Config
{
    [CreateAssetMenu(menuName = "Scene/Block View Animations Config", fileName = "BlockViewAnimationsConfig")]
    public class BlockViewAnimationsConfig : ScriptableObject
    {
        [SerializeField] private SpriteAnimationConfig _idleAnimationConfig;
        [SerializeField] private SpriteAnimationConfig _destroyAnimationConfig;
        
        public SpriteAnimationConfig IdleAnimationConfig => _idleAnimationConfig;
        public SpriteAnimationConfig DestroyAnimationConfig => _destroyAnimationConfig;
    }
}