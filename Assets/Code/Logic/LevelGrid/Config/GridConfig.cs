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
        
        public int Width => _width;
        public int Height => _height;
        
        public BlockIDConfig[] BlockIDConfigs => _blockIDConfigs;

#if UNITY_EDITOR
        private void OnValidate()
        {
            var requiredSize = _width * _height;

            _blockIDConfigs ??= new BlockIDConfig[requiredSize];

            if (BlockIDConfigs.Length == requiredSize)
            {
                return;
            }
            
            var old = BlockIDConfigs;

            _blockIDConfigs = new BlockIDConfig[requiredSize];

            for (var i = 0; i < requiredSize; i++)
            {
                if (old != null && i < old.Length)
                {
                    _blockIDConfigs[i] = old[i];
                }
                else
                {
                    _blockIDConfigs[i] = null;
                }
            }
        }
#endif
    }
}