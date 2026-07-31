using System;
using Code.Logic.LevelBlock.Config;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scene.Config
{
    [Serializable]
    public class BlockViewConfig
    {
        [SerializeField] private BlockIDConfig _idConfig;
        [SerializeField] private BlockView _viewPrefab;
        [FormerlySerializedAs("_animationsConfig")] [SerializeField] private BlockViewAnimationsConfig animationsesConfig;
        
        public  BlockIDConfig IDConfig => _idConfig;
        public BlockView ViewPrefab => _viewPrefab;
        public BlockViewAnimationsConfig AnimationsesConfig => animationsesConfig;
    }
}