using System.Collections.Generic;
using System.Linq;
using Code.Logic.Grids;
using Code.Scene.Block.Config;
using Code.Scene.Block.Contracts;
using Code.Scene.Core.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public sealed class BlockViewController
    {
        private readonly DiContainer _diContainer;
        private readonly BlockViewsConfig _viewsConfig;
        private readonly BlockViewsConfigRepository _configRepository;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly ITickableRegistry _tickableRegistry;
        private readonly IBlockViewLayerService _blockViewLayerService;
        private readonly GridModel _gridModel;
        private readonly IBlockViewSwipeController _blockViewSwipeController;
        
        private Transform _gameFieldTransform;
        
        [Inject]
        public BlockViewController(DiContainer diContainer, BlockViewsConfig viewsConfig, BlockViewsConfigRepository configRepository,
            IBlockViewsRegistry blockViewsRegistry, ITickableRegistry tickableRegistry, 
            IBlockViewLayerService blockViewLayerService, GridModel gridModel, IBlockViewSwipeController blockViewSwipeController)
        {
            _diContainer = diContainer;
            _viewsConfig = viewsConfig;
            _configRepository = configRepository;
            _blockViewsRegistry = blockViewsRegistry;
            _tickableRegistry = tickableRegistry;
            _blockViewLayerService = blockViewLayerService;
            _gridModel = gridModel;
            _blockViewSwipeController = blockViewSwipeController;

            gridModel.OnGridGenerated += OnGridGenerated;
        }
        
        private void CreateBlockViews()
        {
            if (!_gameFieldTransform)
            {
                var gameField = Object.Instantiate(_viewsConfig.LevelFieldPrefab);
                _gameFieldTransform = gameField.transform;
            }

            var blockViewsOld = _blockViewsRegistry.GetViewsAll();
            
            foreach (var blockView in blockViewsOld)
            {
                _tickableRegistry.Unregister(blockView.ID);
                Object.Destroy(blockView.gameObject);
            }
            
            _blockViewsRegistry.Clear();
            
            var cells = _gridModel.Cells.Values.ToArray();

            if (cells.Length == 0)
            {
                return;
            }
            
            var maxX = cells.Max(c => c.X);
            var maxY = cells.Max(c => c.Y);

            var offset = new Vector3(maxX * 0.5f, maxY * 0.5f);
            
            _blockViewSwipeController.UnsubscribeFromAllBlockSwipes();
            
            foreach (var cell in cells)
            {
                if (cell.IsEmpty)
                {
                    continue;
                }
                
                var block = cell.Block;
                var blockPosition = new Vector3(cell.X, cell.Y) - offset;
                var blockViewConfig = _configRepository.Get(block.ConfigID);
                var blockView = _diContainer.InstantiatePrefabForComponent<BlockView>(blockViewConfig.ViewPrefab, 
                    blockPosition, Quaternion.identity, _gameFieldTransform);
                var blockLayer = _blockViewLayerService.GetLayerFromXY(cell.X, cell.Y);
                blockView.Initialize(block.ID.Value, block.ConfigID, blockLayer);
                _blockViewsRegistry.Register(block.ID, blockView);
                _tickableRegistry.Register(block.ID.Value, blockView.SpriteAnimator);
                
                blockView.SpriteAnimator.Play(blockViewConfig.AnimationsConfig.IdleAnimationConfig);
                _blockViewSwipeController.BindToBlockSwipeDetection(blockView);
            }
        }
        
        private void OnGridGenerated(object sender, Dictionary<Vector2Int, GridCell> eventArgs)
        {
            CreateBlockViews();
        }
    }
}