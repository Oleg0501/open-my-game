using System.Collections.Generic;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using Zenject;

namespace Code.Logic.LevelBlock.Implementations
{
    public class BlockMatchDestroyer : IBlockMatchDestroyer
    {
        private readonly GridModel _gridModel;
        private readonly IBlockRegistry _blockRegistry;

        [Inject]
        public BlockMatchDestroyer(GridModel gridModel, IBlockRegistry blockRegistry)
        {
            _gridModel = gridModel;
            _blockRegistry = blockRegistry;
        }
        
        public void DestroyBlocks(HashSet<Block> blocks)
        {
            foreach (var block in blocks)
            {
                var gridCell = _gridModel.GetCellOrNull(block.X, block.Y);

                if (gridCell == null || gridCell.IsEmpty)
                {
                    continue;
                }
                
                _blockRegistry.Unregister(block.ID);
                gridCell.Block = null;
            }
        }
    }
}