using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Code.Logic.Grids;
using Code.Scene.Balloon.Config;
using Code.Scene.Core;
using Code.Scene.Core.Contracts;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Code.Scene.Balloon
{
    public sealed class BalloonViewController
    {
        private readonly DiContainer _diContainer;
        private readonly Camera _camera;
        private readonly BalloonViewsConfig _config;
        private readonly ITickableRegistry _tickableRegistry;

        private readonly List<BalloonView> _views = new();
        
        private Transform _sceneRootTransform;
        private bool _isStarted;
        
        [Inject]
        public BalloonViewController(DiContainer diContainer, [Inject(Id = typeof(CameraInjectID))] Camera camera,
            GridModel gridModel, BalloonViewsConfig config, ITickableRegistry tickableRegistry)
        {
            _diContainer = diContainer;
            _camera = camera;
            _config = config;
            _tickableRegistry = tickableRegistry;

            gridModel.OnGridGenerated += OnGridGenerated;
        }
        
        private void Start()
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
            
            var cancellationToken = new CancellationTokenSource();
            _ = SpawnAsync(cancellationToken.Token);
            
            _isStarted = true;
        }
        
        private async Task SpawnAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_views.Count < _config.MaxSceneAmount)
                {
                    Spawn();
                }

                var seconds = Random.Range(_config.MinSpawnDelay, _config.MaxSpawnDelay);
                var delay = TimeSpan.FromSeconds(seconds);

                await Task.Delay(delay, cancellationToken);
            }
        }
        
        private void Spawn()
        {
            var leftToRight = Random.value > 0.5f;

            var leftSpawnBoundary = _camera.ViewportToWorldPoint(_config.LeftSpawnBoundary);
            var rightSpawnBoundary = _camera.ViewportToWorldPoint(_config.RightSpawnBoundary);
            var bottomSpawnBoundary = _camera.ViewportToWorldPoint(_config.BottomSpawnBoundary);
            var topSpawnBoundary = _camera.ViewportToWorldPoint(_config.TopSpawnBoundary);

            var height = Random.Range(bottomSpawnBoundary.y, topSpawnBoundary.y);
            var config = _config.ViewConfigs[Random.Range(0, _config.ViewConfigs.Length)];
            
            var position = leftToRight 
                ? new Vector3(leftSpawnBoundary.x - 1f, height, 0) 
                : new Vector3(rightSpawnBoundary.x + 1f, height, 0);
            
            var view = _diContainer.InstantiatePrefabForComponent<BalloonView>(config.ViewPrefab, position, Quaternion.identity, _sceneRootTransform);
            view.Initialize(_camera, leftToRight, config.HorizontalSpeed, config.WaveAmplitude, config.WaveFrequency,
                _config.OutOfScreenLeft, _config.OutOfScreenRight);
            view.OnOutOfScreen.AddListener(OnViewOutOfScreen);

            _views.Add(view);
            _tickableRegistry.Register(view.GetInstanceID(), view);
        }

        private void OnViewOutOfScreen(BalloonView view)
        {
            view.OnOutOfScreen.RemoveListener(OnViewOutOfScreen);

            _views.Remove(view);
            _tickableRegistry.Unregister(view.GetInstanceID());
            Object.Destroy(view.gameObject);
        }
        
        private void OnGridGenerated(object sender, Dictionary<Vector2Int, GridCell> eventArgs)
        {
            Start();
        }
    }
}