using Code.Logic.Blocks.Contracts;
using Code.Logic.Grids;
using UnityEngine;
using Zenject;

namespace Code.Logic.Blocks.Implementations
{
    public class BlockDropService : IBlockDropService
    {
        private readonly GridModel _gridModel;

        [Inject]
        public BlockDropService(GridModel gridModel)
        {
            _gridModel = gridModel;
        }

        public void Drop(BlockMovementData movementData)
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
                DropForColumn(x, maxY, movementData);
            }
        }

        private void DropForColumn(int x, int maxY, BlockMovementData movementData)
        {
            var targetY = 0;

            for (var y = 0; y <= maxY; y++)
            {
                var cell = _gridModel.GetCellOrNull(x, y);

                if (cell == null || cell.IsEmpty)
                {
                    continue;
                }
                
                if (y != targetY)
                {
                    var targetCell = _gridModel.GetCellOrNull(x, targetY);
                    var block = cell.Block;

                    cell.Block = null;
                    targetCell.Block = block;

                    var pointFrom = new Vector2Int(block.X, block.Y);

                    block.X = x;
                    block.Y = targetY;
                    
                    var pointTo = new Vector2Int(x, targetY);

                    movementData.Add(block.ID, pointFrom, pointTo);
                }

                targetY++;
            }
        }
    }
}