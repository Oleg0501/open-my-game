using System;
using Code.Logic.LevelGrid;
using Zenject;

namespace Code.Logic.LevelBlock
{
    public class BlockMovementService
    {
        private readonly GridModel _gridModel;

        [Inject]
        public BlockMovementService(GridModel gridModel)
        {
            _gridModel = gridModel;
        }

        public BlockMovementResult Move(Block block, BlockMoveDirection direction)
        {
            var fromCell = _gridModel.GetCellOrNull(block.X, block.Y);

            var deltaX = 0;
            var deltaY = 0;

            switch (direction)
            {
                case BlockMoveDirection.Left:
                    deltaX = -1;
                    break;
                
                case BlockMoveDirection.Right:
                    deltaX = 1;
                    break;
                
                case BlockMoveDirection.Up:
                    deltaY = 1;
                    break;
                
                case BlockMoveDirection.Down:
                    deltaY = -1;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            var toCell = _gridModel.GetCellOrNull(block.X + deltaX, block.Y + deltaY);

            if (toCell == null)
            {
                return null;
            }

            return toCell.Block == null ? MoveBlockToEmpty(fromCell, toCell) : SwapBlocks(fromCell, toCell);
        }
        
        private BlockMovementResult MoveBlockToEmpty(GridCell fromCell, GridCell toCell)
        {
            var block = fromCell.Block;

            fromCell.Block = null;
            toCell.Block = block;

            block.X = toCell.X;
            block.Y = toCell.Y;

            return new BlockMovementResult { FromCell = fromCell, ToCell = toCell, FirstBlock = block, IsSwap = false };
        }
        
        private BlockMovementResult SwapBlocks(GridCell fromCell, GridCell toCell)
        {
            var firstBlock = fromCell.Block;
            var secondBlock = toCell.Block;

            (fromCell.Block, toCell.Block) = (toCell.Block, fromCell.Block);
            (firstBlock.X, secondBlock.X) = (secondBlock.X, firstBlock.X);
            (firstBlock.Y, secondBlock.Y) = (secondBlock.Y, firstBlock.Y);

            return new BlockMovementResult { FromCell = fromCell, ToCell = toCell, FirstBlock = firstBlock, 
                SecondBlock = secondBlock, IsSwap = true };
        }
    }
}