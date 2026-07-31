using UnityEngine;

namespace Code.Scene.Config
{
    [CreateAssetMenu(menuName = "Scene/Input Swipe Config", fileName = "InputSwipeConfig")]
    public class InputSwipeConfig : ScriptableObject
    {
        [SerializeField] private float _minSwipeDetectDistance;
        [SerializeField] private float _maxSwipeDetectDistance;
        
        public float MinSwipeDetectDistance => _minSwipeDetectDistance;
        public float MaxSwipeDetectDistance => _maxSwipeDetectDistance;
    }
}