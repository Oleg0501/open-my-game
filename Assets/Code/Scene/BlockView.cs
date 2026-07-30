using UnityEngine;
using UnityEngine.Events;

namespace Code.Scene
{
    [RequireComponent(typeof(InputSwipeDetector))]
    public class BlockView : MonoBehaviour
    {
        public int ID { get; private set; }
        public InputSwipeDetector InputSwipeDetector { get; private set; }
        public UnityEvent<int, Vector2> OnSwiped { get; set; } = new();

        private void Awake()
        {
            InputSwipeDetector = GetComponent<InputSwipeDetector>();
            InputSwipeDetector.OnSwiped.AddListener(OnInputDetectorSwiped);
        }
        
        public void Initialize(int id)
        {
            ID = id;
        }
        
        private void OnInputDetectorSwiped(Vector2 eventArgs)
        {
            OnSwiped?.Invoke(ID, eventArgs);
        }
    }
}