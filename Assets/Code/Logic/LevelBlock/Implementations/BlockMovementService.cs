using System;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using Zenject;

namespace Code.Logic.LevelBlock.Implementations
{
    public class BlockMovementService : IBlockMovementService
    {
        private readonly GridModel _gridModel;
        private readonly BlockRegistry _blockRegistry;

        [Inject]
        public BlockMovementService(GridModel gridModel, BlockRegistry blockRegistry)
        {
            _gridModel = gridModel;
            _blockRegistry = blockRegistry;
        }

        public bool TryMove(BlockID blockID, BlockMovementDirection direction, out BlockMovementResult blockMovementResult)
        {
            blockMovementResult = null;
            
            var block = _blockRegistry.GetBlock(blockID);
            var fromCell = _gridModel.GetCellOrNull(block.X, block.Y);

            if (fromCell == null)
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

            blockMovementResult = toCell.Block == null 
                ? MoveBlockToEmpty(fromCell, toCell) 
                : SwapBlocks(fromCell, toCell);
            
            return true;
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