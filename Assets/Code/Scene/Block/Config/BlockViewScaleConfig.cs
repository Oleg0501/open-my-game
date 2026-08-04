using UnityEngine;

namespace Code.Scene.Block.Config
{
    [CreateAssetMenu(menuName = "Scene/Block View Scale Config", fileName = "BlockViewScaleConfig")]
    public class BlockViewScaleConfig : ScriptableObject
    {
        [SerializeField] private float _horizontalPadding;
        [SerializeField] private float _maxScale;

        public float HorizontalPadding => _horizontalPadding;
        public float MaxScale => _maxScale;
    }
}