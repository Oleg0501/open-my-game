using System.Collections.Generic;
using Code.Logic.LevelBlock;
using UnityEngine;

namespace Code.Logic.LevelGrid
{
    public class GridModel
    {
        public Dictionary<Vector2Int, GridCell> Cells { get; } = new();

        public void GenerateLevel(GridConfig gridConfig)
        {
            Cells.Clear();
            
            var blockConfigs = gridConfig.BlockConfigs;
            var blockConfigIndex = 0;
            
            for (var i = 0; i < gridConfig.Width; i++)
            {
                for (var j = 0; j < gridConfig.Height; j++)
                {
                    var block = new Block(i, j, blockConfigs[blockConfigIndex]);
                    var cell = new GridCell(i, j, block);
                    blockConfigIndex++;
                    
                    var position = new Vector2Int(i, j);
                    Cells.Add(position, cell);
                }
            }
        }
        
        public GridCell GetCellOrNull(int x,int y)
        {
            if (Cells.TryGetValue(new Vector2Int(x, y), out var cell))
            {
                return cell;
            }
            
            Debug.LogError($"GridModel, grid cell at position {x}, {y} not found");
            
            return null;
        }
    }
}