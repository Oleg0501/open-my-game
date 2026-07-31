using UnityEngine;

namespace Code.Logic.LevelGrid.Config
{
    [CreateAssetMenu(menuName = "Logic/Level Config", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private GridConfig[] _gridConfigs;
        
        public GridConfig[] GridConfigs => _gridConfigs;
    }
}