using System;
using Code.Logic.Blocks.Config;
using UnityEngine;

namespace Code.Scene.Block.Config
{
    [Serializable]
    public class BlockViewConfig
    {
        [SerializeField] private BlockIDConfig _idConfig;
        [SerializeField] private BlockView _viewPrefab;
        [SerializeField] private BlockViewAnimationsConfig _animationsConfig;
        
        public  BlockIDConfig IDConfig => _idConfig;
        public BlockView ViewPrefab => _viewPrefab;
        public BlockViewAnimationsConfig AnimationsConfig => _animationsConfig;
    }
}