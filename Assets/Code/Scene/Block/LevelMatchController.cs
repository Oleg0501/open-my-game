using System.Collections.Generic;
using System.Threading;
using Code.Logic.Blocks;
using Code.Logic.Blocks.Contracts;
using Code.Scene.Block.Config;
using Code.Scene.Block.Contracts;
using Code.Scene.Core.Contracts;
using Cysharp.Threading.Tasks;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Scene.Block
{
    public sealed class LevelMatchController
    {
        private readonly IBlockRegistry _blockRegistry;
        private readonly IBlockDropService _blockDropService;
        private readonly IBlockMatchFinder _blockMatchFinder;
        private readonly IBlockMatchDestroyer _blockMatchDestroyer;
        private readonly BlockViewsConfig _blockViewsConfig;

        private readonly BlockViewsConfigRepository _blockViewsConfigRepository;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly IBlockViewMovementService _blockViewMovementService;
        
        private readonly ITickableRegistry _tickableRegistry;
        
        [Inject]
        public LevelMatchController(IBlockRegistry blockRegistry, IBlockDropService blockDropService,
            IBlockMatchFinder blockMatchFinder, IBlockMatchDestroyer blockMatchDestroyer,
            BlockViewsConfig blockViewsConfig, BlockViewsConfigRepository blockViewsConfigRepository, 
            IBlockViewsRegistry blockViewsRegistry, IBlockViewMovementService blockViewMovementService, 
            ITickableRegistry tickableRegistry)
        {
            _blockRegistry = blockRegistry;
            _blockDropService = blockDropService;
            _blockMatchFinder = blockMatchFinder;
            _blockMatchDestroyer = blockMatchDestroyer;
            _blockViewsConfig = blockViewsConfig;
            _blockViewsConfigRepository = blockViewsConfigRepository;
            _blockViewsRegistry = blockViewsRegistry;
            _blockViewMovementService = blockViewMovementService;
            _tickableRegistry = tickableRegistry;
        }
        
        public async UniTask MatchAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                await DropBlocksAsync(cancellationToken);

                var matches = _blockMatchFinder.FindMatches();

                if (matches.Count == 0)
                {
                    return;
                }

                _blockMatchDestroyer.DestroyBlocks(matches);

                await DestroyMatchedBlockViewsAsync(cancellationToken);
            }
        }
        
        private async UniTask DropBlocksAsync(CancellationToken cancellationToken)
        {
            var movementResult = new BlockMovementData(); 
            _blockDropService.Drop(movementResult); 
            
            await _blockViewMovementService.MoveAsync(movementResult, _blockViewsConfig.DropSpeed, cancellationToken);
        }
        
        private async UniTask DestroyMatchedBlockViewsAsync(CancellationToken cancellationToken)
        {
            var viewsToDestroy = GetViewsToDestroy();

            await PlayDestroyAnimationsAsync(viewsToDestroy, cancellationToken);

            DestroyViews(viewsToDestroy);
        }
        
        private async UniTask PlayDestroyAnimationsAsync(IReadOnlyCollection<BlockView> views, CancellationToken cancellationToken)
        {
            var tasks = new List<UniTask>();

            foreach (var view in views)
            {
                view.SetInputLock(true);
                
                var config = _blockViewsConfigRepository.Get(view.ConfigID);

                tasks.Add(view.SpriteAnimator.PlayAndWaitAsync(config.AnimationsConfig.DestroyAnimationConfig, cancellationToken));
            }

            await UniTask.WhenAll(tasks);
        }
        
        private List<BlockView> GetViewsToDestroy()
        {
            var result = new List<BlockView>();

            foreach (var view in _blockViewsRegistry.GetViewsAll())
            {
                if (_blockRegistry.Contains(new BlockID(view.ID)))
                {
                    continue;
                }

                result.Add(view);
            }

            return result;
        }
        
        private void DestroyViews(IEnumerable<BlockView> views)
        {
            foreach (var view in views)
            {
                if (!view)
                {
                    continue;
                }

                _blockViewsRegistry.Unregister(new BlockID(view.ID));
                _tickableRegistry.Unregister(view.ID);

                Object.Destroy(view.gameObject);
            }
        }
    }
}