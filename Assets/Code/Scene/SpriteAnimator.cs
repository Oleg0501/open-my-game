using Code.Scene.Config;
using UnityEngine;

namespace Code.Scene
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private SpriteAnimationConfig _config;
        private float _timer;
        private int _frame;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnDestroy()
        {
            Stop();
        }

        public void Play(SpriteAnimationConfig config)
        {
            _config = config;
            _spriteRenderer.sprite = config.Frames[0];
            _timer = 0;
            _frame = 0;
        }

        public void Stop()
        {
            _config = null;
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
                        
                        return;
                    }
                }
                
                _spriteRenderer.sprite = _config.Frames[_frame];
            }
        }
    }
}