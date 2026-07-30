using Code.Logic.LevelBlock;
using UnityEngine;

namespace Code.Logic.LevelGrid
{
    [CreateAssetMenu(menuName = "Logic/Grid Config", fileName = "GridConfig")]
    public class GridConfig : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private BlockConfig[] _blockConfigs;
        
        public int Width => _width;
        public int Height => _height;
        
        public BlockConfig[] BlockConfigs => _blockConfigs;

#if UNITY_EDITOR
        private void OnValidate()
        {
            var requiredSize = _width * _height;

            _blockConfigs ??= new BlockConfig[requiredSize];

            if (BlockConfigs.Length == requiredSize)
            {
                return;
            }
            
            var old = BlockConfigs;

            _blockConfigs = new BlockConfig[requiredSize];

            for (var i = 0; i < requiredSize; i++)
            {
                if (old != null && i < old.Length)
                {
                    _blockConfigs[i] = old[i];
                }
                else
                {
                    _blockConfigs[i] = null;
                }
            }
        }
#endif
    }
}