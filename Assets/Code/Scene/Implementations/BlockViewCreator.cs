using System.Collections.Generic;
using System.Linq;
using Code.Logic.LevelGrid;
using Code.Scene.Config;
using Code.Scene.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Implementations
{
    public class BlockViewCreator : IBlockViewCreator
    {
        private readonly BlockViewsConfig _viewsConfig;
        private readonly BlockViewsConfigRepository _configRepository;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly GridModel _gridModel;
        private readonly IBlockViewSwipeController _blockViewSwipeController;
        
        private Transform _gameFieldTransform;
        
        [Inject]
        public BlockViewCreator(BlockViewsConfig viewsConfig, BlockViewsConfigRepository configRepository, 
            IBlockViewsRegistry blockViewsRegistry, GridModel gridModel, IBlockViewSwipeController blockViewSwipeController)
        {
            _viewsConfig = viewsConfig;
            _configRepository = configRepository;
            _blockViewsRegistry = blockViewsRegistry;
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
            
            for (var i = 0; i < _gameFieldTransform.childCount; i++)
            {
                var childTransform = _gameFieldTransform.GetChild(i);
                Object.Destroy(childTransform.gameObject);
            }
            
            var cells = _gridModel.Cells.Values.ToArray();

            if (cells.Length == 0)
            {
                return;
            }
            
            var maxX = cells.Max(c => c.X);
            var maxY = cells.Max(c => c.Y);

            var offset = new Vector2(maxX * 0.5f, maxY * 0.5f);
            
            _blockViewsRegistry.Clear();
            _blockViewSwipeController.UnsubscribeFromAllBlockSwipes();
            
            foreach (var cell in cells)
            {
                if (cell.IsEmpty)
                {
                    continue;
                }
                
                var block = cell.Block;
                var blockPosition = new Vector2(cell.X, cell.Y) - offset;
                var blockViewConfig = _configRepository.Get(block.ConfigID.ID);
                var blockView = Object.Instantiate(blockViewConfig.ViewPrefab, blockPosition, Quaternion.identity, _gameFieldTransform);
                blockView.Initialize(block.ID.Value);
                
                _blockViewsRegistry.Register(block.ID, blockView);
                _blockViewSwipeController.BindToBlockSwipeDetection(blockView);
            }
        }
        
        private void OnGridGenerated(object sender, Dictionary<Vector2Int, GridCell> eventArgs)
        {
            CreateBlockViews();
        }
    }
}