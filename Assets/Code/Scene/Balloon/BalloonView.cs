using UnityEngine;
using UnityEngine.Events;

namespace Code.Scene.Balloon
{
    public class BalloonView : MonoBehaviour
    {
        private Camera _camera;
        private float _direction;
        private float _horizontalSpeed;
        private float _waveAmplitude;
        private float _waveFrequency;
        
        private float _baseY;
        private float _startX;
        private float _time;

        public UnityEvent<BalloonView> OnOutOfScreen = new();

        public void Initialize(Camera cam, bool moveRight, float horizontalSpeed, float waveAmplitude, float waveFrequency)
        {
            _camera = cam;
            _direction = moveRight ? 1f : -1f;
            _horizontalSpeed = horizontalSpeed;
            _waveAmplitude = waveAmplitude;
            _waveFrequency = waveFrequency;
            
            _startX = transform.position.x;
            _baseY = transform.position.y;
            
            _time = 0f;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            var x = _startX + _direction * _horizontalSpeed * _time;
            var y = _baseY + Mathf.Sin(_time * _waveFrequency) * _waveAmplitude;

            transform.position = new Vector3(x, y, transform.position.z);

            if (IsOutOfScreen())
            {
                OnOutOfScreen?.Invoke(this);
            }
        }

        private bool IsOutOfScreen()
        {
            var viewport = _camera.WorldToViewportPoint(transform.position);

            return viewport.x is < -0.2f or > 1.2f;
        }
    }
}