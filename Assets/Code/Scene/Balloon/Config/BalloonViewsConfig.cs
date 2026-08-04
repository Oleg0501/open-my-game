using UnityEngine;

namespace Code.Scene.Balloon.Config
{
    [CreateAssetMenu(menuName = "Scene/Balloon Views Config", fileName = "BalloonViewsConfig")]
    public class BalloonViewsConfig : ScriptableObject
    {
        [SerializeField] private GameObject _sceneRootPrefab;
        [SerializeField] private int _maxSceneAmount;
        [SerializeField] private float _minSpawnDelay;
        [SerializeField] private float _maxSpawnDelay;
        [SerializeField] private Vector3 _leftSpawnBoundary;
        [SerializeField] private Vector3 _rightSpawnBoundary;
        [SerializeField] private Vector3 _bottomSpawnBoundary;
        [SerializeField] private Vector3 _topSpawnBoundary;
        [SerializeField] private float _outOfScreenLeft;
        [SerializeField] private float _outOfScreenRight;
        [SerializeField] private BalloonViewConfig[] _viewConfigs;
        
        public GameObject SceneRootPrefab => _sceneRootPrefab;
        public int MaxSceneAmount => _maxSceneAmount;
        public float MinSpawnDelay => _minSpawnDelay;
        public float MaxSpawnDelay => _maxSpawnDelay;
        public Vector3 LeftSpawnBoundary => _leftSpawnBoundary;
        public Vector3 RightSpawnBoundary => _rightSpawnBoundary;
        public Vector3 BottomSpawnBoundary => _bottomSpawnBoundary;
        public Vector3 TopSpawnBoundary => _topSpawnBoundary;
        public float OutOfScreenLeft => _outOfScreenLeft;
        public float OutOfScreenRight => _outOfScreenRight;
        public BalloonViewConfig[] ViewConfigs => _viewConfigs;
    }
}