using System;
using System.Collections.Generic;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid.Config;
using UnityEngine;
using Zenject;

namespace Code.Logic.LevelGrid
{
    public class GridModel
    {
        private readonly IBlockIDGenerator _blockIDGenerator;
        private readonly IBlockRegistry _blockRegistry;
        
        public EventHandler<Dictionary<Vector2Int, GridCell>> OnGenerated =  delegate { };
        public Dictionary<Vector2Int, GridCell> Cells { get; } = new();

        [Inject]
        public GridModel(IBlockIDGenerator blockIDGenerator, IBlockRegistry blockRegistry)
        {
            _blockIDGenerator = blockIDGenerator;
            _blockRegistry = blockRegistry;
        }
        
        public void Generate(GridConfig gridConfig)
        {
            Cells.Clear();
            _blockRegistry.Clear();
            _blockIDGenerator.Reset();
            
            var blockIDConfigs = gridConfig.BlockIDConfigs;
            var iterationIndex = 0;
            
            for (var i = 0; i < gridConfig.Width; i++)
            {
                for (var j = 0; j < gridConfig.Height; j++)
                {
                    Block block = null;
                    var blockIDConfig = blockIDConfigs[iterationIndex];

                    if (blockIDConfig)
                    {
                        var blockID = new BlockID(_blockIDGenerator.Next());
                        block = new Block(blockID, blockIDConfig, i, j);
                        _blockRegistry.Register(blockID, block);
                    }
                    
                    var cell = new GridCell(i, j, block);
                    var position = new Vector2Int(i, j);
                    Cells.Add(position, cell);
                    
                    iterationIndex++;
                }
            }
            
            OnGenerated?.Invoke(this, Cells);
        }
        
        public GridCell GetCellOrNull(int x,int y)
        {
            return Cells.GetValueOrDefault(new Vector2Int(x, y));
        }
    }
}