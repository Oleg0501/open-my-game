using UnityEngine;

namespace Code.Scene.Config
{
    [CreateAssetMenu(menuName = "Scene/Balloon Views Config", fileName = "BalloonViewsConfig")]
    public class BalloonViewsConfig : ScriptableObject
    {
        [SerializeField] private GameObject _sceneRootPrefab;
        [SerializeField] private int _maxSceneAmount;
        [SerializeField] private float _minSpawnDelay;
        [SerializeField] private float _maxSpawnDelay;
        [SerializeField] private BalloonViewConfig[] _viewConfigs;
        
        public GameObject SceneRootPrefab => _sceneRootPrefab;
        public int MaxSceneAmount => _maxSceneAmount;
        public float MinSpawnDelay => _minSpawnDelay;
        public float MaxSpawnDelay => _maxSpawnDelay;
        public BalloonViewConfig[] ViewConfigs => _viewConfigs;
    }
}