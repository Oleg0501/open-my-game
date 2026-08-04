using Code.Logic.Blocks;
using Code.Logic.Blocks.Contracts;
using Code.Scene.Block.Contracts;
using Code.Scene.Config;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewSwipeController : IBlockViewSwipeController
    {
        private readonly InputSwipeConfig _inputSwipeConfig;
        private readonly IBlockMovementService _blockMovementService;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockViewMovementService _blockViewMovementService;
        private readonly BlockViewMatchController _blockViewMatchController;

        [Inject]
        public BlockViewSwipeController(InputSwipeConfig inputSwipeConfig, IBlockMovementService blockMovementService, 
            IBlockViewsRegistry blockViewsRegistry, IBlockViewLayerService blockViewLayerService, 
            IBlockViewMovementService blockViewMovementService, BlockViewMatchController blockViewMatchController)
        {
            _inputSwipeConfig = inputSwipeConfig;
            _blockMovementService = blockMovementService;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewMovementService = blockViewMovementService;
            _blockViewMatchController = blockViewMatchController;
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
            var swipeMovementResult = new BlockMovementData();
            var movementDirection = BlockMovementDirectionHelper.GetNormalizedDirection(swipeDirection);
            
            if (!_blockMovementService.TryMove(new BlockID(id), movementDirection, swipeMovementResult))
            {
                return;
            }
            
            await _blockViewMovementService.MoveAsync(swipeMovementResult);
            await _blockViewMatchController.MatchAsync();
        }
    }
}