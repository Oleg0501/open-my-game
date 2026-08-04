using System.Threading.Tasks;
using Code.Scene.Config;
using Code.Scene.Core.Contracts;
using UnityEngine;

namespace Code.Scene.SpriteAnimator
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour, ITickable
    {
        private SpriteRenderer _spriteRenderer;

        private SpriteAnimationConfig _config;
        private float _timer;
        private int _frame;
        private TaskCompletionSource<bool> _taskCompletionSource;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            _config = null;
        }

        public void Play(SpriteAnimationConfig config)
        {
            _config = config;
            _spriteRenderer.sprite = config.Frames[0];
            _timer = 0;
            _frame = 0;
        }

        public async Task PlayAndWaitAsync(SpriteAnimationConfig config)
        {
            if (config.IsLoop)
            {
                return;
            }
            
            Play(config);
            _taskCompletionSource = new TaskCompletionSource<bool>();
            
            await _taskCompletionSource.Task;
        }
        
        public void Tick(float deltaTime)
        {
            if (_config == null)
            {
                return;
            }
            
            _timer += deltaTime;
            
            var frameDuration = 1f / _config.FPS;

            while (_timer >= frameDuration)
            {
                _timer -= frameDuration;
                _frame++;
                
                if (_frame >= _config.Frames.Length)
                {
                    if (_config.IsLoop)
                    {
                        _frame = 0;
                    }
                    else
                    {
                        _frame = _config.Frames.Length - 1;
                        _spriteRenderer.sprite = _config.Frames[_frame];
                        
                        _taskCompletionSource?.SetResult(true);
                        
                        return;
                    }
                }
                
                _spriteRenderer.sprite = _config.Frames[_frame];
            }
        }
    }
}