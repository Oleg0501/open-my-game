using UnityEngine;

namespace Code.Logic.LevelBlock
{
    [CreateAssetMenu(menuName = "Logic/Block ID Config", fileName = "BlockIDConfig")]
    public class BlockIDConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        
        public string ID => _id;
    }
}