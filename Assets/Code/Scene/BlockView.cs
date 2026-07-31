using UnityEngine;
using UnityEngine.Events;

namespace Code.Scene
{
    [RequireComponent(typeof(InputSwipeDetector))]
    public class BlockView : MonoBehaviour
    {
        public InputSwipeDetector InputSwipeDetector { get; private set; }
        public UnityEvent<int, Vector2> OnSwiped { get; set; } = new();

        private int _id;

        private void Awake()
        {
            InputSwipeDetector = GetComponent<InputSwipeDetector>();
            InputSwipeDetector.OnSwiped.AddListener(OnInputDetectorSwiped);
        }
        
        public void Initialize(int id)
        {
            _id = id;
        }
        
        private void OnInputDetectorSwiped(Vector2 eventArgs)
        {
            OnSwiped?.Invoke(_id, eventArgs);
        }
    }
}