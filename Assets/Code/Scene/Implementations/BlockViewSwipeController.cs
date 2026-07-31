using System.Linq;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using Code.Scene.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Implementations
{
    public class BlockViewSwipeController : IBlockViewSwipeController
    {
        private readonly GridModel _gridModel;
        private readonly IBlockMovementService _blockMovementService;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        
        [Inject]
        public BlockViewSwipeController(GridModel gridModel, IBlockMovementService blockMovementService, 
            IBlockViewsRegistry blockViewsRegistry)
        {
            _gridModel = gridModel;
            _blockMovementService = blockMovementService;
            _blockViewsRegistry = blockViewsRegistry;
        }

        public void SubscribeOnBlockSwipe(BlockID blockID)
        {
            var blockView = _blockViewsRegistry.GetView(blockID);
            blockView.OnSwiped.AddListener(OnSwiped);
        }

        public void UnsubscribeFromAllBlockSwipes()
        {
            var blockViews = _blockViewsRegistry.GetViewsAll();
            
            foreach (var blockView in blockViews)
            {
                blockView.OnSwiped.RemoveListener(OnSwiped);
            }
        }

        private void OnSwiped(int id, Vector2 swipeDirection)
        {
            var movementDirection = BlockMovementDirectionHelper.GetNormalizedDirection(swipeDirection);

            if (!_blockMovementService.TryMove(new BlockID(id), movementDirection, out var blockMovementResult))
            {
                return;
            }
            
            var firstBlockView = _blockViewsRegistry.GetView(blockMovementResult.FirstBlock.ID);
            var secondBlockView = blockMovementResult.IsSwap 
                ? _blockViewsRegistry.GetView(blockMovementResult.SecondBlock.ID) 
                : null;
            
            // var secondBlockView = _blockViewsRegistry.GetView(blockMovementResult.SecondBlock.ID);
            
            var maxX = _gridModel.Cells.Values.ToArray().Max(c => c.X);
            var maxY = _gridModel.Cells.Values.ToArray().Max(c => c.Y);

            var offset = new Vector3(maxX * 0.5f, maxY * 0.5f);
            
            firstBlockView.transform.position = new Vector3(blockMovementResult.FirstBlock.X, blockMovementResult.FirstBlock.Y) - offset;

            if (secondBlockView)
            {
                secondBlockView.transform.position = new Vector3(blockMovementResult.SecondBlock.X, blockMovementResult.SecondBlock.Y) - offset;
            }
        }
    }
}