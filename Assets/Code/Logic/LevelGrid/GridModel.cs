using System.Collections.Generic;
using Code.Logic.LevelBlock;

namespace Code.Logic.LevelGrid
{
    public class GridModel
    {
        private readonly Dictionary<int, GridCell> _cells = new();
        
        public Dictionary<int, GridCell> Cells => _cells;
        
        public void GenerateLevel(GridConfig gridConfig)
        {
            _cells.Clear();
            
            var blockConfigs = gridConfig.BlockConfigs;
            var blockConfigIndex = 0;
            
            for (var i = 0; i < gridConfig.Width; i++)
            {
                for (var j = 0; j < gridConfig.Height; j++)
                {
                    var block = new Block(i, j, blockConfigs[blockConfigIndex]);
                    var cell = new GridCell(i, j, block);
                    blockConfigIndex++;
                    
                    _cells.Add(blockConfigIndex, cell);
                }
            }
        }
    }
}