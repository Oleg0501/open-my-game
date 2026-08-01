using Code.Scene.Contracts;
using Code.Scene.SpriteAnimator.Contracts;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scene.Block
{
    [RequireComponent(typeof(InputSwipeDetector))]
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteAnimator.SpriteAnimator _spriteAnimator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public int ID { get; private set; }
        public InputSwipeDetector InputSwipeDetector { get; private set; }
        public SpriteAnimator.SpriteAnimator SpriteAnimator => _spriteAnimator;
        public UnityEvent<int, Vector2> OnSwiped { get; set; } = new();

        private ISpriteAnimatorsRegistry _spriteAnimatorsRegistry;

        private void Awake()
        {
            InputSwipeDetector = GetComponent<InputSwipeDetector>();
            InputSwipeDetector.OnSwiped.AddListener(OnInputDetectorSwiped);
        }
        
        public void Initialize(int id, int layer)
        {
            ID = id;
            SetLayer(layer);
        }

        public void SetLayer(int layer)
        {
            _spriteRenderer.sortingOrder = layer;
        }
        
        private void OnInputDetectorSwiped(Vector2 eventArgs)
        {
            OnSwiped?.Invoke(ID, eventArgs);
        }
    }
}