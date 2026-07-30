using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Code.Scene
{
    public class InputSwipeDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent<Vector2> OnSwiped { get; set; } = new();

        private Vector2 _pointerDownStartPosition;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerDownStartPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var pointerUpPosition = eventData.position;
            var delta =  pointerUpPosition - _pointerDownStartPosition;

            if (delta.sqrMagnitude < 30)
            {
                return;
            }
            
            OnSwiped?.Invoke(delta);
        }
    }
}