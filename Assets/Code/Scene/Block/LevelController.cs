using System;
using System.Linq;
using Code.Logic.Grids;
using Code.Scene.Block.Config;
using Code.Scene.Block.Contracts;
using Code.Scene.Core.Contracts;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Scene.Block
{
    public sealed class LevelController
    {
        private readonly DiContainer _diContainer;
        
        private readonly GridModel _gridModel;
        
        private readonly BlockViewsConfig _blockViewsConfig;
        private readonly BlockViewsConfigRepository _blockViewsConfigRepository;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockViewsScaleService _blockViewsScaleService;
        private readonly IBlockViewLayerService _blockViewLayerService;
        private readonly IBlockViewSwipeController _blockViewSwipeController;
        
        private readonly ITickableRegistry _tickableRegistry;
        private readonly ICancellationTokenService _cancellationTokenService;

        private Transform _sceneRootTransform;
        
        [Inject]
        public LevelController(DiContainer diContainer, GridModel gridModel, BlockViewsConfig blockBlockViewsConfig,
            BlockViewsConfigRepository blockViewsViewsBlockViewsConfigRepository, IBlockViewsRegistry blockViewsRegistry, 
            IBlockViewsScaleService blockViewsScaleService, IBlockViewLayerService blockViewLayerService,
            IBlockViewSwipeController blockViewSwipeController, ITickableRegistry tickableRegistry, 
            ICancellationTokenService cancellationTokenService)
        {
            _diContainer = diContainer;
            _gridModel = gridModel;
            _blockViewsConfig = blockBlockViewsConfig;
            _blockViewsConfigRepository = blockViewsViewsBlockViewsConfigRepository;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewsScaleService = blockViewsScaleService;
            _blockViewLayerService = blockViewLayerService;
            _blockViewSwipeController = blockViewSwipeController;
            _tickableRegistry = tickableRegistry;
            _cancellationTokenService = cancellationTokenService;
            
            gridModel.OnGridGenerated += OnGridGenerated;
        }
        
        private void InitializeLevel()
        {
            CreateSceneRoot();
            UpdateSceneRootScale();
            DestroyOldBlockViews();

            var cells = _gridModel.Cells.Values.ToArray();

            if (cells.Length == 0)
            {
                return;
            }

            _blockViewSwipeController.UnsubscribeFromAllBlockViewSwipes();

            CreateBlockViews(cells);
        }
        
        private void CreateSceneRoot()
        {
            if (_sceneRootTransform)
            {
                return;
            }

            var sceneRoot = Object.Instantiate(_blockViewsConfig.SceneRootPrefab);
            _sceneRootTransform = sceneRoot.transform;
        }
        
        private void CreateBlockViews(GridCell[] cells)
        {
            var offset = new Vector3((_gridModel.Width - 1) * 0.5f, (_gridModel.Height - 1) * 0.5f, 0f);

            foreach (var cell in cells)
            {
                if (cell.IsEmpty)
                {
                    continue;
                }

                CreateBlockView(cell, offset);
            }
        }
        
        private void CreateBlockView(GridCell cell, Vector3 offset)
        {
            var block = cell.Block;
            var config = _blockViewsConfigRepository.Get(block.ConfigID);
            var layer = _blockViewLayerService.GetLayerFromXY(cell.X, cell.Y);

            var blockView = _diContainer.InstantiatePrefabForComponent<BlockView>(config.ViewPrefab, _sceneRootTransform);
            blockView.Initialize(block.ID.Value, block.ConfigID, layer);
            blockView.transform.localPosition = new Vector3(cell.X, cell.Y) - offset;
            blockView.SpriteAnimator.Play(config.AnimationsConfig.IdleAnimationConfig);

            _blockViewsRegistry.Register(block.ID, blockView);
            _tickableRegistry.Register(block.ID.Value, blockView.SpriteAnimator);
            _blockViewSwipeController.BindToBlockViewSwipeDetection(blockView);
        }
        
        private void UpdateSceneRootScale()
        {
            var scale = _blockViewsScaleService.GetSceneRootScale(_gridModel.Width, _gridModel.Height);
            _sceneRootTransform.localScale = Vector3.one * scale;
        }
        
        private void DestroyOldBlockViews()
        {
            foreach (var blockView in _blockViewsRegistry.GetViewsAll())
            {
                _tickableRegistry.Unregister(blockView.ID);
                Object.Destroy(blockView.gameObject);
            }

            _blockViewsRegistry.Clear();
        }
        
        private void OnGridGenerated(object sender, EventArgs eventArgs)
        {
            _cancellationTokenService.Cancel();
            _cancellationTokenService.Reset();
            
            InitializeLevel();
        }
    }
}