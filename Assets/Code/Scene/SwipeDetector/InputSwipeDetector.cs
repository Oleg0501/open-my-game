using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Code.Scene.SwipeDetector
{
    public class InputSwipeDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent<Vector2> OnSwiped { get; set; } = new();

        private Vector2 _pointerDownStartPosition;

        private float _minSwipeDistance;
        private float _maxSwipeDistance;
        
        public void Initialize(float minSwipeDistance, float maxSwipeDistance)
        {
            _minSwipeDistance = minSwipeDistance;
            _maxSwipeDistance = maxSwipeDistance;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDownStartPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var pointerUpPosition = eventData.position;
            var delta =  pointerUpPosition - _pointerDownStartPosition;

            if (delta.sqrMagnitude < _minSwipeDistance || delta.sqrMagnitude > _maxSwipeDistance)
            {
                return;
            }
            
            OnSwiped?.Invoke(delta);
        }
    }
}