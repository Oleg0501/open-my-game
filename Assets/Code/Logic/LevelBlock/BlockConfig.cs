using UnityEngine;

namespace Code.Logic.LevelBlock
{
    [CreateAssetMenu(menuName = "Logic/Block Config", fileName = "BlockConfig")]
    public class BlockConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Prefab => _prefab;
    }
}