using Code.Scene.Core.Contracts;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scene.Balloon
{
    public class BalloonView : MonoBehaviour, ITickable
    {
        [HideInInspector] public UnityEvent<BalloonView> OnOutOfScreen = new();
        
        private Camera _camera;
        private float _direction;
        private float _horizontalSpeed;
        private float _waveAmplitude;
        private float _waveFrequency;
        private float _outOfScreenLeft;
        private float _outOfScreenRight;
        
        private float _startX;
        private float _startY;
        private float _time;
        
        public void Initialize(Camera cam, bool moveRight, float horizontalSpeed, float waveAmplitude, float waveFrequency,
            float outOfScreenLeft, float outOfScreenRight)
        {
            _camera = cam;
            _direction = moveRight ? 1f : -1f;
            _horizontalSpeed = horizontalSpeed;
            _waveAmplitude = waveAmplitude;
            _waveFrequency = waveFrequency;
            _outOfScreenLeft = outOfScreenLeft;
            _outOfScreenRight = outOfScreenRight;
            
            _startX = transform.position.x;
            _startY = transform.position.y;
            _time = 0f;
        }

        public void Tick(float deltaTime)
        {
            _time += deltaTime;

            var x = _startX + _direction * _horizontalSpeed * _time;
            var y = _startY + Mathf.Sin(_time * _waveFrequency) * _waveAmplitude;

            transform.position = new Vector3(x, y, transform.position.z);

            if (IsOutOfScreen())
            {
                OnOutOfScreen?.Invoke(this);
            }
        }
        
        private bool IsOutOfScreen()
        {
            var viewport = _camera.WorldToViewportPoint(transform.position);

            return viewport.x < _outOfScreenLeft || viewport.x > _outOfScreenRight;
        }
    }
}