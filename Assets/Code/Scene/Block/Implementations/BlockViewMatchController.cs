using System.Collections.Generic;
using Code.Logic.Blocks;
using Code.Logic.Blocks.Contracts;
using Code.Scene.Block.Config;
using Code.Scene.Block.Contracts;
using Code.Scene.Core.Contracts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public sealed class BlockViewMatchController
    {
        private readonly IBlockDropService _blockDropService;
        private readonly IBlockViewMovementService _blockViewMovementService;
        private readonly IBlockMatchFinder _blockMatchFinder;
        private readonly IBlockMatchDestroyer _blockMatchDestroyer;
        private readonly IBlockRegistry _blockRegistry;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly ITickableRegistry _tickableRegistry;
        private readonly BlockViewsConfigRepository _blockViewsConfigRepository;

        [Inject]
        public BlockViewMatchController(IBlockDropService blockDropService, IBlockViewMovementService blockViewMovementService, 
            IBlockMatchFinder blockMatchFinder, IBlockMatchDestroyer blockMatchDestroyer, IBlockRegistry blockRegistry, 
            IBlockViewsRegistry blockViewsRegistry, ITickableRegistry tickableRegistry, 
            BlockViewsConfigRepository blockViewsConfigRepository)
        {
            _blockDropService = blockDropService;
            _blockViewMovementService = blockViewMovementService;
            _blockMatchFinder = blockMatchFinder;
            _blockMatchDestroyer = blockMatchDestroyer;
            _blockRegistry = blockRegistry;
            _blockViewsRegistry = blockViewsRegistry;
            _tickableRegistry = tickableRegistry;
            _blockViewsConfigRepository = blockViewsConfigRepository;
        }
        
        public async UniTask MatchAsync()
        {
            while (true)
            {
                var blockMovementResult = new BlockMovementData();
                _blockDropService.Drop(blockMovementResult);
                
                await _blockViewMovementService.MoveAsync(blockMovementResult);

                var matches = _blockMatchFinder.FindMatches();

                if (matches.Count == 0)
                {
                    break;
                }
                
                _blockMatchDestroyer.DestroyBlocks(matches);
                await SynchronizeBlockViews();
            }
        }
        
        public async UniTask SynchronizeBlockViews()
        {
            var allViews = _blockViewsRegistry.GetViewsAll();
            var syncViews = new List<BlockView>();

            foreach (var view in allViews)
            {
                var blockID = new BlockID(view.ID);
        
                if (_blockRegistry.Contains(blockID))
                {
                    continue;
                }
                
                syncViews.Add(view);
            }
            
            var tasks = new List<UniTask>();
        
            foreach (var view in syncViews)
            {
                var viewConfig = _blockViewsConfigRepository.Get(view.ConfigID);
                tasks.Add(view.SpriteAnimator.PlayAndWaitAsync(viewConfig.AnimationsConfig.DestroyAnimationConfig));
            }
            
            await UniTask.WhenAll(tasks);
        
            foreach (var view in syncViews)
            {
                _blockViewsRegistry.Unregister(new BlockID(view.ID));
                _tickableRegistry.Unregister(view.ID);
                Object.Destroy(view.gameObject);
            }
        }
    }
}