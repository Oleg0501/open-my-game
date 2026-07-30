using System.Linq;
using Code.Logic.LevelGrid;
using UnityEngine;
using Zenject;

namespace Code.Logic.LevelBlock
{
    public class BlockGenerator
    {
        private readonly GridModel _gridModel;
        private readonly LevelModel _levelModel;
        private readonly LevelConfig _levelConfig;

        private Transform _gameFieldTransform;
        
        [Inject]
        public BlockGenerator(GridModel gridModel, LevelModel levelModel, LevelConfig levelConfig)
        {
            _gridModel = gridModel;
            _levelModel = levelModel;
            _levelConfig = levelConfig;
        }
        
        public void StartNextLevel()
        {
            var levelConfig = _levelModel.NextLevelConfig();
            _gridModel.GenerateLevel(levelConfig);

            if (!_gameFieldTransform)
            {
                var gameField = Object.Instantiate(_levelConfig.LevelFieldPrefab);
                _gameFieldTransform = gameField.transform;
            }
            
            InstantiateBlocks();
        }

        public void RestartCurrentLevel()
        {
            InstantiateBlocks();
        }

        private void InstantiateBlocks()
        {
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
            
            foreach (var cell in cells)
            {
                var blockPosition = new Vector2(cell.X, cell.Y) - offset;
                Object.Instantiate(cell.Block.BlockConfig.Prefab, blockPosition, Quaternion.identity, _gameFieldTransform);
            }
        }
    }
}