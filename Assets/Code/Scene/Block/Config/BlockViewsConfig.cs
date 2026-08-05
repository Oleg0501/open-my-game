using UnityEngine;

namespace Code.Scene.Block.Config
{
    [CreateAssetMenu(menuName = "Scene/Block Views Config", fileName = "BlockViewsConfig")]
    public class BlockViewsConfig : ScriptableObject
    {
        [SerializeField] private GameObject _sceneRootPrefab;
        [SerializeField] private float _swipeSpeed;
        [SerializeField] private float _dropSpeed;
        [SerializeField] private BlockViewConfig[] _viewConfigs;

        public GameObject SceneRootPrefab => _sceneRootPrefab;
        public float SwipeSpeed => _swipeSpeed;
        public float DropSpeed => _dropSpeed;
        public BlockViewConfig[] ViewConfigs => _viewConfigs;
    }
}