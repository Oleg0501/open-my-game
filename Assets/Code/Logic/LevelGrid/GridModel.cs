using System;
using System.Collections.Generic;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelBlock.Implementations;
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
            
            var blockConfigs = gridConfig.BlockIDConfigs;
            var blockConfigIndex = 0;
            
            for (var i = 0; i < gridConfig.Width; i++)
            {
                for (var j = 0; j < gridConfig.Height; j++)
                {
                    var blockId = new BlockID(_blockIDGenerator.Next());
                    var blockConfigId = blockConfigs[blockConfigIndex];
                    var block = new Block(blockId, blockConfigId, i, j);
                    _blockRegistry.Register(blockId, block);
                    
                    var cell = new GridCell(i, j, block);
                    blockConfigIndex++;
                    
                    var position = new Vector2Int(i, j);
                    Cells.Add(position, cell);
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