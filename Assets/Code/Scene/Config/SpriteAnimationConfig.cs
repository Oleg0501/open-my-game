using System;
using UnityEngine;

namespace Code.Scene.Config
{
    [Serializable]
    public class SpriteAnimationConfig
    {
        [SerializeField] private int _fps;
        [SerializeField] private bool _isLoop;
        [SerializeField] private Sprite[] _frames;
        
        public int FPS => _fps;
        public bool IsLoop => _isLoop;
        public Sprite[] Frames => _frames;
    }
}