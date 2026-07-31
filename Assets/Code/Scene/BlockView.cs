using Code.Scene.Contracts;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scene
{
    [RequireComponent(typeof(InputSwipeDetector))]
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteAnimator _spriteAnimator;

        public int ID { get; private set; }
        public InputSwipeDetector InputSwipeDetector { get; private set; }
        public SpriteAnimator SpriteAnimator => _spriteAnimator;
        public UnityEvent<int, Vector2> OnSwiped { get; set; } = new();

        private ISpriteAnimatorsRegistry _spriteAnimatorsRegistry;

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