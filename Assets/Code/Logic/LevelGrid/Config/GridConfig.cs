using Code.Logic.LevelBlock.Config;
using UnityEngine;

namespace Code.Logic.LevelGrid.Config
{
    [CreateAssetMenu(menuName = "Logic/Grid Config", fileName = "GridConfig")]
    public class GridConfig : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private BlockIDConfig[] _blockIDConfigs;
        
        private string[] _cachedBlockConfigIDs;
        
        public int Width => _width;
        public int Height => _height;
        public string[] CachedBlockConfigIDs => _cachedBlockConfigIDs;

        private void OnEnable()
        {
            RebuildCacheArray();
        }
        
        private void RebuildCacheArray()
        {
            _cachedBlockConfigIDs = new string[_blockIDConfigs.Length];

            for (var i = 0; i < _blockIDConfigs.Length; i++)
            {
                _cachedBlockConfigIDs[i] = _blockIDConfigs[i] != null ? _blockIDConfigs[i].ID : string.Empty;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ResizeArray();
            RebuildCacheArray();
        }
        
        private void ResizeArray()
        {
            var requiredSize = _width * _height;

            _blockIDConfigs ??= new BlockIDConfig[requiredSize];

            if (_blockIDConfigs.Length == requiredSize)
            {
                return;
            }

            var old = _blockIDConfigs;
            _blockIDConfigs = new BlockIDConfig[requiredSize];

            for (var i = 0; i < requiredSize; i++)
            {
                if (i < old.Length)
                {
                    _blockIDConfigs[i] = old[i];
                }
            }
        }
#endif
    }
}