using System.Threading.Tasks;
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
        public UnityEvent<int, Vector2> OnSwiped { get; } = new();
        
        private bool _isMoving;
        
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
        
        public async Task MoveToAsync(Vector3 targetPosition, float duration)
        {
            _isMoving = true;
            
            var startPosition = transform.position;

            if (duration <= 0f)
            {
                transform.position = targetPosition;
                
                return;
            }

            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);

                transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                await Task.Yield();
            }

            transform.position = targetPosition;
            _isMoving = false;
        }
        
        private void OnInputDetectorSwiped(Vector2 eventArgs)
        {
            if (_isMoving)
            {
                return;
            }
            
            OnSwiped?.Invoke(ID, eventArgs);
        }
    }
}