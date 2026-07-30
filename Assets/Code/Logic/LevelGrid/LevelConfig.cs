using UnityEngine;

namespace Code.Logic.LevelGrid
{
    [CreateAssetMenu(menuName = "Logic/Level Config", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private GameObject _levelFieldPrefab;
        [SerializeField] private GridConfig[] _gridConfigs;
        
        public GameObject LevelFieldPrefab => _levelFieldPrefab;
        public GridConfig[] GridConfigs => _gridConfigs;
    }
}