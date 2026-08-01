using System.Collections;
using System.Collections.Generic;
using Code.Logic.LevelGrid;
using Code.Scene.Config;
using UnityEngine;
using Zenject;

namespace Code.Scene.Balloon.Implementations
{
    public class BalloonViewController
    {
        private readonly DiContainer _diContainer;
        private readonly Camera _camera;
        private readonly BalloonViewsConfig _config;

        private readonly List<BalloonView> _views = new();
        
        private Coroutine _coroutine;
        private Transform _sceneRootTransform;
        private bool _isStarted;
        
        [Inject]
        public BalloonViewController(DiContainer diContainer, GridModel gridModel, [Inject(Id = "Camera")] Camera camera, 
            BalloonViewsConfig config)
        {
            _diContainer = diContainer;
            _camera = camera;
            _config = config;
            
            gridModel.OnGenerated += OnGridGenerated;
        }
        
        public void Start()
        {
            if (_isStarted)
            {
                return;
            }
            
            if (!_sceneRootTransform)
            {
                var sceneRootTransform = Object.Instantiate(_config.SceneRootPrefab);
                _sceneRootTransform = sceneRootTransform.transform;
            }
            
            _coroutine = CoroutineRunner.Instance.StartCoroutine(SpawnCoroutine());
            _isStarted = true;
        }
        
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                if (_views.Count <= _config.MaxSceneAmount)
                {
                    Spawn();
                }
                
                yield return new WaitForSeconds(Random.Range(_config.MinSpawnDelay, _config.MaxSpawnDelay));
            }
        }

        private void Spawn()
        {
            var leftToRight = Random.value > 0.5f;

            var leftBoundary = _camera.ViewportToWorldPoint(new Vector3(0, 0, 10));
            var rightBoundary = _camera.ViewportToWorldPoint(new Vector3(1, 0, 10));
            var bottomBoundary = _camera.ViewportToWorldPoint(new Vector3(0, 0, 10));
            var topBoundary = _camera.ViewportToWorldPoint(new Vector3(0, 1, 10));

            var height = Random.Range(bottomBoundary.y, topBoundary.y);
            var config = _config.ViewConfigs[Random.Range(0, _config.ViewConfigs.Length)];
            var position = leftToRight ? new Vector3(leftBoundary.x - 1f, height, 0) : new Vector3(rightBoundary.x + 1f, height, 0);
            
            var view = _diContainer.InstantiatePrefabForComponent<BalloonView>(config.BalloonViewPrefab, position, 
                Quaternion.identity, _sceneRootTransform);
            view.Initialize(_camera, leftToRight, config.HorizontalSpeed, config.WaveAmplitude, config.WaveFrequency);
            view.OnOutOfScreen.AddListener(OnViewOutOfScreen);

            _views.Add(view);
        }

        private void OnViewOutOfScreen(BalloonView view)
        {
            view.OnOutOfScreen.RemoveListener(OnViewOutOfScreen);

            _views.Remove(view);
            Object.Destroy(view.gameObject);
        }
        
        private void OnGridGenerated(object sender, Dictionary<Vector2Int, GridCell> eventArgs)
        {
            Start();
        }
    }
}