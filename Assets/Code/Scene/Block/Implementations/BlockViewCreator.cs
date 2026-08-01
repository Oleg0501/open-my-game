using System.Collections.Generic;
using System.Linq;
using Code.Logic.LevelGrid;
using Code.Scene.Block.Contracts;
using Code.Scene.Config;
using Code.Scene.Contracts;
using Code.Scene.SpriteAnimator.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewCreator : IBlockViewCreator
    {
        private readonly DiContainer _diContainer;
        private readonly BlockViewsConfig _viewsConfig;
        private readonly BlockViewsConfigRepository _configRepository;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly ISpriteAnimatorsRegistry _spriteAnimatorsRegistry;
        private readonly IBlockViewLayerService _blockViewLayerService;
        private readonly GridModel _gridModel;
        private readonly IBlockViewSwipeController _blockViewSwipeController;
        
        private Transform _gameFieldTransform;
        
        [Inject]
        public BlockViewCreator(DiContainer diContainer, BlockViewsConfig viewsConfig, BlockViewsConfigRepository configRepository, 
            IBlockViewsRegistry blockViewsRegistry, ISpriteAnimatorsRegistry spriteAnimatorsRegistry, 
            IBlockViewLayerService blockViewLayerService, GridModel gridModel, IBlockViewSwipeController blockViewSwipeController)
        {
            _diContainer = diContainer;
            _viewsConfig = viewsConfig;
            _configRepository = configRepository;
            _blockViewsRegistry = blockViewsRegistry;
            _spriteAnimatorsRegistry = spriteAnimatorsRegistry;
            _blockViewLayerService = blockViewLayerService;
            _gridModel = gridModel;
            _blockViewSwipeController = blockViewSwipeController;

            gridModel.OnGenerated += OnGridGenerated;
        }
        
        public void CreateBlockViews()
        {
            if (!_gameFieldTransform)
            {
                var gameField = Object.Instantiate(_viewsConfig.LevelFieldPrefab);
                _gameFieldTransform = gameField.transform;
            }

            var blockViewsOld = _blockViewsRegistry.GetViewsAll();
            
            foreach (var blockView in blockViewsOld)
            {
                _spriteAnimatorsRegistry.Unregister(blockView.ID);
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
                blockView.Initialize(block.ID.Value, blockLayer);
                _blockViewsRegistry.Register(block.ID, blockView);
                _spriteAnimatorsRegistry.Register(block.ID.Value, blockView.SpriteAnimator);
                
                blockView.SpriteAnimator.Play(blockViewConfig.AnimationsesConfig.IdleAnimationConfig);
                _blockViewSwipeController.BindToBlockSwipeDetection(blockView);
            }
        }
        
        private void OnGridGenerated(object sender, Dictionary<Vector2Int, GridCell> eventArgs)
        {
            CreateBlockViews();
        }
    }
}