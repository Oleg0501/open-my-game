using System;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Config;
using UnityEngine;

namespace Code.Scene.Config
{
    [Serializable]
    public class BlockViewConfig
    {
        [SerializeField] private BlockIDConfig _idConfig;
        [SerializeField] private BlockView _viewPrefab;
        
        public  BlockIDConfig IDConfig => _idConfig;
        public BlockView ViewPrefab => _viewPrefab;
    }
}