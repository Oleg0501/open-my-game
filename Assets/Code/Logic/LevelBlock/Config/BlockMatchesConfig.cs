using UnityEngine;

namespace Code.Logic.LevelBlock.Config
{
    [CreateAssetMenu(menuName = "Logic/Block Matches Config", fileName = "BlockMatchesConfig")]
    public class BlockMatchesConfig : ScriptableObject
    {
        [SerializeField] private BlockMatchConfig[] _matchConfigs;
        public BlockMatchConfig[] MatchConfigs => _matchConfigs;
    }
}