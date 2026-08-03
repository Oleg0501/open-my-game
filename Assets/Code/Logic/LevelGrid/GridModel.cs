using System;
using System.Collections.Generic;
using System.Linq;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid.Config;
using Code.Logic.Storage.Implementations;
using UnityEngine;
using Zenject;

namespace Code.Logic.LevelGrid
{
    public class GridModel : BaseJsonStorage<GridData>
    {
        private readonly IBlockIDGenerator _blockIDGenerator;
        private readonly IBlockRegistry _blockRegistry;
     
        public override string SaveKey => "GridStorage";
        
        public EventHandler<Dictionary<Vector2Int, GridCell>> OnGenerated =  delegate { };
        public Dictionary<Vector2Int, GridCell> Cells { get; } = new();
        
        public int Width { get; private set; }
        public int Height { get; private set; }

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
            
            Width = gridConfig.Width;
            Height = gridConfig.Height;
            
            var blockConfigIDs = Data?.BlockConfigIDs ?? gridConfig.CachedBlockConfigIDs;
            var iterationIndex = 0;
            
            for (var i = 0; i < gridConfig.Width; i++)
            {
                for (var j = 0; j < gridConfig.Height; j++)
                {
                    Block block = null;
                    var blockConfigID = blockConfigIDs[iterationIndex];

                    if (!string.IsNullOrEmpty(blockConfigID))
                    {
                        var blockID = new BlockID(_blockIDGenerator.Next());
                        block = new Block(blockID, blockConfigID, i, j);
                        _blockRegistry.Register(blockID, block);
                    }
                    
                    var cell = new GridCell(i, j, block);
                    var position = new Vector2Int(i, j);
                    Cells.Add(position, cell);
                    
                    iterationIndex++;
                }
            }
            
            OnGenerated?.Invoke(this, Cells);
            
            if (Data != null)
            {
                Data.BlockConfigIDs = null;
            }
        }
        
        public GridCell GetCellOrNull(int x,int y)
        {
            return Cells.GetValueOrDefault(new Vector2Int(x, y));
        }
        
        public override void Save()
        {
            Data.BlockConfigIDs = Cells.Values.Select(cell => !cell.IsEmpty ? cell.Block.ConfigID : "").ToArray();
            base.Save();
        }
    }
}