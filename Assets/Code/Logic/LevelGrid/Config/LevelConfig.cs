using Code.Logic.LevelGrid.Config;
using UnityEngine;

namespace Code.Logic.LevelGrid
{
    [CreateAssetMenu(menuName = "Logic/Level Config", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private GridConfig[] _gridConfigs;
        
        public GridConfig[] GridConfigs => _gridConfigs;
    }
}