using System;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using UnityEngine;
using Zenject;

namespace Code.Logic.LevelBlock.Implementations
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

        public bool TryMove(BlockID blockID, BlockMovementDirection direction, BlockMovementResult result)
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
                    MoveBlockToEmpty(fromCell, toCell, result);
                    break;
                default:
                    SwapBlocks(fromCell, toCell, result);
                    break;
            }

            return true;
        }
        
        private void MoveBlockToEmpty(GridCell fromCell, GridCell toCell, BlockMovementResult result)
        {
            var block = fromCell.Block;

            fromCell.Block = null;
            toCell.Block = block;

            block.X = toCell.X;
            block.Y = toCell.Y;
            
            result.Add(block.ID, new Vector2Int(fromCell.X, fromCell.Y), new Vector2Int(toCell.X, toCell.Y));
        }
        
        private void SwapBlocks(GridCell fromCell, GridCell toCell, BlockMovementResult result)
        {
            var firstBlock = fromCell.Block;
            var secondBlock = toCell.Block;

            (fromCell.Block, toCell.Block) = (toCell.Block, fromCell.Block);
            (firstBlock.X, secondBlock.X) = (secondBlock.X, firstBlock.X);
            (firstBlock.Y, secondBlock.Y) = (secondBlock.Y, firstBlock.Y);
            
            result.Add(firstBlock.ID, new Vector2Int(fromCell.X, fromCell.Y),  new Vector2Int(toCell.X, toCell.Y));
            result.Add(secondBlock.ID, new Vector2Int(toCell.X, toCell.Y), new Vector2Int(fromCell.X, fromCell.Y));
        }
    }
}