using System;
using System.Collections.Generic;
using System.Linq;
using Code.Logic.Blocks;
using Code.Logic.Blocks.Config;
using Code.Logic.Blocks.Contracts;
using Code.Logic.Grids.Config;
using Code.Logic.Storage.Implementations;
using UnityEngine;
using Zenject;

namespace Code.Logic.Grids
{
    public sealed class GridModel : BaseJsonStorage<GridData>
    {
        protected override string SaveKey => "GridStorage";
        
        public EventHandler<Dictionary<Vector2Int, GridCell>> OnGridGenerated =  delegate { };
        public Dictionary<Vector2Int, GridCell> Cells { get; } = new();
        
        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly IBlockIDGenerator _blockIDGenerator;
        private readonly IBlockRegistry _blockRegistry;
        
        private BlockIDConfig[] _blockConfigIDs;

        [Inject]
        public GridModel(IBlockIDGenerator blockIDGenerator, IBlockRegistry blockRegistry)
        {
            _blockIDGenerator = blockIDGenerator;
            _blockRegistry = blockRegistry;
        }
        
        public void GenerateGrid(GridConfig gridConfig)
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
                        var blockID = _blockIDGenerator.Next();
                        block = new Block(blockID, blockConfigID, i, j);
                        _blockRegistry.Register(blockID, block);
                    }
                    
                    var cell = new GridCell(i, j, block);
                    var position = new Vector2Int(i, j);
                    Cells.Add(position, cell);
                    
                    iterationIndex++;
                }
            }
            
            OnGridGenerated?.Invoke(this, Cells);
            
            if (Data != null)
            {
                Data.BlockConfigIDs = null;
            }
        }
        
        public GridCell GetCellOrNull(int x, int y)
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