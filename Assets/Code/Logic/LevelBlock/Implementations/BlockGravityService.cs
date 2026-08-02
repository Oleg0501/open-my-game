using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using UnityEngine;
using Zenject;

namespace Code.Logic.LevelBlock.Implementations
{
    public class BlockGravityService : IBlockGravityService
    {
        private readonly GridModel _gridModel;

        [Inject]
        public BlockGravityService(GridModel gridModel)
        {
            _gridModel = gridModel;
        }

        public void ApplyGravity(BlockMovementResult result)
        {
            var maxX = 0;
            var maxY = 0;

            foreach (var cell in _gridModel.Cells.Values)
            {
                if (cell.X > maxX)
                {
                    maxX = cell.X;
                }

                if (cell.Y > maxY)
                {
                    maxY = cell.Y;
                }
            }

            for (var x = 0; x <= maxX; x++)
            {
                ApplyGravityForColumn(x, maxY, result);
            }
        }

        private void ApplyGravityForColumn(int x, int maxY, BlockMovementResult result)
        {
            var targetY = 0;

            for (var y = 0; y <= maxY; y++)
            {
                var cell = _gridModel.GetCellOrNull(x, y);

                if (cell == null)
                {
                    continue;
                }

                if (cell.IsEmpty)
                {
                    continue;
                }

                if (y != targetY)
                {
                    var targetCell = _gridModel.GetCellOrNull(x, targetY);

                    var block = cell.Block;

                    cell.Block = null;
                    targetCell.Block = block;

                    var from = new Vector2Int(block.X, block.Y);

                    block.X = x;
                    block.Y = targetY;

                    result.Add(block.ID, from, new Vector2Int(x, targetY));
                }

                targetY++;
            }
        }
    }
}