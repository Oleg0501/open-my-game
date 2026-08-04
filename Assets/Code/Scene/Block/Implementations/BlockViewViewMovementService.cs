using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Logic.Blocks;
using Code.Logic.Grids;
using Code.Scene.Block.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewViewMovementService : IBlockViewMovementService
    {
        private readonly GridModel _gridModel;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockViewLayerService _blockViewLayerService;

        [Inject]
        public BlockViewViewMovementService(GridModel gridModel, IBlockViewsRegistry blockViewsRegistry, 
            IBlockViewLayerService blockViewLayerService)
        {
            _gridModel = gridModel;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewLayerService = blockViewLayerService;
        }
        
        public async Task MoveAsync(BlockMovementData blockMovementData)
        {
            var tasks = new List<Task>();
            var moves = blockMovementData.Movements;

            var maxX = _gridModel.Cells.Values.ToArray().Max(c => c.X);
            var maxY = _gridModel.Cells.Values.ToArray().Max(c => c.Y);
            
            var offset = new Vector3(maxX * 0.5f, maxY * 0.5f);
            
            foreach (var move in moves)
            {
                var blockView = _blockViewsRegistry.GetView(move.BlockID);

                var target = new Vector3(move.ToPoint.x, move.ToPoint.y, 0) - offset;
                
                var layer = _blockViewLayerService.GetLayerFromXY(move.ToPoint.x, move.ToPoint.y);
                blockView.SetLayer(layer);

                tasks.Add(blockView.MoveToAsync(target, 0.75f));
            }
            
            await Task.WhenAll(tasks);
        }
    }
}