using Code.Logic.Blocks;
using Code.Logic.Blocks.Contracts;
using Code.Scene.Block.Config;
using Code.Scene.Block.Contracts;
using Code.Scene.Config;
using Code.Scene.Core.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public class BlockViewSwipeController : IBlockViewSwipeController
    {
        private readonly InputSwipeConfig _inputSwipeConfig;
        private readonly BlockViewsConfig _blockViewsConfig;
        
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockMovementService _blockMovementService;
        private readonly IBlockViewMovementService _blockViewMovementService;
        
        private readonly LevelMatchController _levelMatchController;
        
        private readonly ICancellationTokenService _cancellationTokenService;
        
        [Inject]
        public BlockViewSwipeController(BlockViewsConfig blockViewsConfig, InputSwipeConfig inputSwipeConfig, IBlockMovementService blockMovementService, 
            IBlockViewsRegistry blockViewsRegistry, IBlockViewMovementService blockViewMovementService, LevelMatchController levelMatchController,
            ICancellationTokenService cancellationTokenService)
        {
            _inputSwipeConfig = inputSwipeConfig;
            _blockViewsConfig = blockViewsConfig;
            _blockMovementService = blockMovementService;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewMovementService = blockViewMovementService;
            _levelMatchController = levelMatchController;
            _cancellationTokenService = cancellationTokenService;
        }
        
        public void BindToBlockViewSwipeDetection(BlockView blockView)
        {
            blockView.InitializeSwipeDetector(_inputSwipeConfig.MinSwipeDetectDistance, _inputSwipeConfig.MaxSwipeDetectDistance);
            blockView.OnSwiped.AddListener(OnSwiped);
        }

        public void UnsubscribeFromAllBlockViewSwipes()
        {
            foreach (var blockView in _blockViewsRegistry.GetViewsAll())
            {
                blockView.OnSwiped.RemoveListener(OnSwiped);
            }
        }

        private async void OnSwiped(int id, Vector2 swipeDirection)
        {
            var direction = BlockMovementDirectionHelper.GetNormalizedDirection(swipeDirection);
            var movement = new BlockMovementData();

            if (!_blockMovementService.TryMove(new BlockID(id), direction, movement))
            {
                return;
            }

            var cancellationToken = _cancellationTokenService.Token;
            await _blockViewMovementService.MoveAsync(movement, _blockViewsConfig.SwipeSpeed, cancellationToken);
            await _levelMatchController.MatchAsync(cancellationToken);
        }
    }
}