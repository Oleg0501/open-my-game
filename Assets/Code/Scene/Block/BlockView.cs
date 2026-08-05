using System;
using System.Threading;
using Code.Scene.SwipeDetector;
using Cysharp.Threading.Tasks;
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
        public string ConfigID { get; private set; }
        public InputSwipeDetector InputSwipeDetector { get; private set; }
        public SpriteAnimator.SpriteAnimator SpriteAnimator => _spriteAnimator;
        public UnityEvent<int, Vector2> OnSwiped { get; } = new();
        
        private bool _isInputLock;
        
        private void Awake()
        {
            InputSwipeDetector = GetComponent<InputSwipeDetector>();
            InputSwipeDetector.OnSwiped.AddListener(OnInputDetectorSwiped);
        }
        
        public void Initialize(int id, string configId, int layer)
        {
            ID = id;
            ConfigID = configId;
            SetLayer(layer);
        }

        public void SetLayer(int layer)
        {
            _spriteRenderer.sortingOrder = layer;
        }

        public void SetInputLock(bool isLock)
        {
            _isInputLock = isLock;
        }
        
        public async UniTask MoveToAsync(Vector3 targetPosition, float duration, CancellationToken cancellationToken)
        {
            SetInputLock(true);
            
            var startPosition = transform.localPosition;

            if (duration <= 0f)
            {
                transform.localPosition = targetPosition;
                
                return;
            }

            var elapsed = 0f;

            try
            {
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    var t = Mathf.Clamp01(elapsed / duration);

                    transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

                    await UniTask.Yield(cancellationToken);
                }

                transform.localPosition = targetPosition;
                SetInputLock(false);
            }
            catch (OperationCanceledException exception)
            {
            }
        }
        
        private void OnInputDetectorSwiped(Vector2 eventArgs)
        {
            if (_isInputLock)
            {
                return;
            }
            
            OnSwiped?.Invoke(ID, eventArgs);
        }
    }
}