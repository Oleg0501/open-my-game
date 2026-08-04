using System;
using Code.Logic.Blocks.Contracts;
using Code.Logic.Grids;
using UnityEngine;
using Zenject;

namespace Code.Logic.Blocks.Implementations
{
    public class BlockMovementService : IBlockMovementService
    {
        private readonly GridModel _gridModel;
        private readonly IBlockRegistry _blockRegistry;

        [Inject]
        public BlockMovementService(GridModel gridModel, IBlockRegistry blockRegistry)
        {
            _gridModel = gridModel;
            _blockRegistry = blockRegistry;
        }

        public bool TryMove(BlockID blockID, BlockMovementDirection direction, BlockMovementData data)
        {
            var block = _blockRegistry.GetBlock(blockID);
            var fromCell = _gridModel.GetCellOrNull(block.X, block.Y);

            if (fromCell == null || fromCell.IsEmpty)
            {
                return false;
            }

            var deltaX = 0;
            var deltaY = 0;

            switch (direction)
            {
                case BlockMovementDirection.Left:
                    deltaX = -1;
                    break;
                
                case BlockMovementDirection.Right:
                    deltaX = 1;
                    break;
                
                case BlockMovementDirection.Up:
                    deltaY = 1;
                    break;
                
                case BlockMovementDirection.Down:
                    deltaY = -1;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            var toCell = _gridModel.GetCellOrNull(block.X + deltaX, block.Y + deltaY);

            if (toCell == null)
            {
                return false;
            }

            switch (toCell.IsEmpty)
            {
                case true when direction == BlockMovementDirection.Up:
                    return false;
                
                case true:
                    MoveToEmpty(fromCell, toCell, data);
                    break;
                
                default:
                    Swap(fromCell, toCell, data);
                    break;
            }

            return true;
        }
        
        private void MoveToEmpty(GridCell fromCell, GridCell toCell, BlockMovementData data)
        {
            var block = fromCell.Block;

            fromCell.Block = null;
            toCell.Block = block;

            block.X = toCell.X;
            block.Y = toCell.Y;

            var fromPoint = new Vector2Int(fromCell.X, fromCell.Y);
            var toPoint = new Vector2Int(toCell.X, toCell.Y);
            
            data.Add(block.ID, fromPoint, toPoint);
        }
        
        private void Swap(GridCell fromCell, GridCell toCell, BlockMovementData data)
        {
            var firstBlock = fromCell.Block;
            var secondBlock = toCell.Block;

            (fromCell.Block, toCell.Block) = (toCell.Block, fromCell.Block);
            (firstBlock.X, secondBlock.X) = (secondBlock.X, firstBlock.X);
            (firstBlock.Y, secondBlock.Y) = (secondBlock.Y, firstBlock.Y);
            
            var fromPoint = new Vector2Int(fromCell.X, fromCell.Y);
            var toPoint = new Vector2Int(toCell.X, toCell.Y);
            
            data.Add(firstBlock.ID, fromPoint,  toPoint);
            data.Add(secondBlock.ID, toPoint, fromPoint);
        }
    }
}