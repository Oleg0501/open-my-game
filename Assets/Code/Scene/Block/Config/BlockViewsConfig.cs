using UnityEngine;

namespace Code.Scene.Block.Config
{
    [CreateAssetMenu(menuName = "Scene/Block Views Config", fileName = "BlockViewsConfig")]
    public class BlockViewsConfig : ScriptableObject
    {
        [SerializeField] private GameObject _levelFieldPrefab;
        [SerializeField] private BlockViewConfig[] _viewConfigs;

        public GameObject LevelFieldPrefab => _levelFieldPrefab;
        public BlockViewConfig[] ViewConfigs => _viewConfigs;
    }
}