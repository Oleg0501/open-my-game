using System;
using UnityEngine;

namespace Code.Scene.Config
{
    [Serializable]
    public class BalloonViewConfig
    {
        [SerializeField] private BalloonView _balloonViewPrefab;
        
        [Header("Movement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _waveAmplitude;
        [SerializeField] private float _waveFrequency;
        
        public BalloonView BalloonViewPrefab => _balloonViewPrefab;
        public float HorizontalSpeed => _horizontalSpeed;
        public float WaveAmplitude => _waveAmplitude;
        public float WaveFrequency => _waveFrequency;
    }
}