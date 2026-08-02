using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using Code.Scene.Block.Contracts;
using Code.Scene.Config;
using Code.Scene.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewSwipeController : IBlockViewSwipeController
    {
        private readonly InputSwipeConfig _inputSwipeConfig;
        private readonly GridModel _gridModel;
        private readonly IBlockMovementService _blockMovementService;
        private readonly IBlockGravityService _blockGravityService;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockViewMovementService _blockViewMovementService;

        [Inject]
        public BlockViewSwipeController(InputSwipeConfig inputSwipeConfig, GridModel gridModel, 
            IBlockMovementService blockMovementService, IBlockGravityService blockGravityService, IBlockViewsRegistry blockViewsRegistry, 
            IBlockViewLayerService blockViewLayerService, IBlockViewMovementService blockViewMovementService)
        {
            _inputSwipeConfig = inputSwipeConfig;
            _gridModel = gridModel;
            _blockMovementService = blockMovementService;
            _blockGravityService = blockGravityService;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewMovementService = blockViewMovementService;
        }

        public void BindToBlockSwipeDetection(BlockView blockView)
        {
            blockView.InputSwipeDetector.Initialize(_inputSwipeConfig.MinSwipeDetectDistance, _inputSwipeConfig.MaxSwipeDetectDistance);
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

        private async void OnSwiped(int id, Vector2 swipeDirection)
        {
            var swipeMovementResult = new BlockMovementResult();
            var movementDirection = BlockMovementDirectionHelper.GetNormalizedDirection(swipeDirection);
            
            if (!_blockMovementService.TryMove(new BlockID(id), movementDirection, swipeMovementResult))
            {
                return;
            }
            
            await _blockViewMovementService.MoveAsync(swipeMovementResult);
            
            var gravityMovementResult = new BlockMovementResult();
            _blockGravityService.ApplyGravity(gravityMovementResult);
            await _blockViewMovementService.MoveAsync(gravityMovementResult);
        }
    }
}