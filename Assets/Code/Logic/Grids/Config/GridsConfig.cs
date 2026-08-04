using UnityEngine;

namespace Code.Logic.Grids.Config
{
    [CreateAssetMenu(menuName = "Logic/Grids Config", fileName = "GridsConfig")]
    public class GridsConfig : ScriptableObject
    {
        [SerializeField] private GridConfig[] _configs;
        
        public GridConfig[] Configs => _configs;
    }
}