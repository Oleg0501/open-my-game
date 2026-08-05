using System.Collections.Generic;
using System.Threading;
using Code.Logic.Blocks;
using Code.Logic.Grids;
using Code.Scene.Block.Contracts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewMovementService : IBlockViewMovementService
    {
        private readonly GridModel _gridModel;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockViewLayerService _blockViewLayerService;

        [Inject]
        public BlockViewMovementService(GridModel gridModel, IBlockViewsRegistry blockViewsRegistry, IBlockViewLayerService blockViewLayerService)
        {
            _gridModel = gridModel;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewLayerService = blockViewLayerService;
        }
        
        public async UniTask MoveAsync(BlockMovementData blockMovementData, CancellationToken cancellationToken)
        {
            var tasks = new List<UniTask>();
            var moves = blockMovementData.Movements;
            var offset = new Vector3((_gridModel.Width - 1) * 0.5f, (_gridModel.Height - 1) * 0.5f, 0f);
            
            foreach (var move in moves)
            {
                var blockView = _blockViewsRegistry.GetView(move.BlockID);

                var target = new Vector3(move.ToPoint.x, move.ToPoint.y, 0) - offset;
                
                var layer = _blockViewLayerService.GetLayerFromXY(move.ToPoint.x, move.ToPoint.y);
                blockView.SetLayer(layer);

                tasks.Add(blockView.MoveToAsync(target, 0.75f, cancellationToken));
            }
            
            await UniTask.WhenAll(tasks);
        }
    }
}