using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Scene.Block.Contracts;
using Code.Scene.Config;
using Code.Scene.SpriteAnimator.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Scene.Block.Implementations
{
    public sealed class BlockViewMatchController
    {
        private readonly IBlockGravityService _blockGravityService;
        private readonly IBlockViewMovementService _blockViewMovementService;
        private readonly IBlockMatchFinder _blockMatchFinder;
        private readonly IBlockMatchDestroyer _blockMatchDestroyer;
        private readonly IBlockRegistry _blockRegistry;
        private readonly IBlockViewsRegistry _blockViewsRegistry;
        private readonly ISpriteAnimatorsRegistry _spriteAnimatorsRegistry;
        private readonly BlockViewsConfigRepository _blockViewsConfigRepository;

        [Inject]
        public BlockViewMatchController(IBlockGravityService blockGravityService, IBlockViewMovementService blockViewMovementService, 
            IBlockMatchFinder blockMatchFinder, IBlockMatchDestroyer blockMatchDestroyer, IBlockRegistry blockRegistry, 
            IBlockViewsRegistry blockViewsRegistry, ISpriteAnimatorsRegistry spriteAnimatorsRegistry, 
            BlockViewsConfigRepository blockViewsConfigRepository)
        {
            _blockGravityService = blockGravityService;
            _blockViewMovementService = blockViewMovementService;
            _blockMatchFinder = blockMatchFinder;
            _blockMatchDestroyer = blockMatchDestroyer;
            _blockRegistry = blockRegistry;
            _blockViewsRegistry = blockViewsRegistry;
            _spriteAnimatorsRegistry = spriteAnimatorsRegistry;
            _blockViewsConfigRepository = blockViewsConfigRepository;
        }
        
        public async Task MatchAsync()
        {
            while (true)
            {
                var blockMovementResult = new BlockMovementResult();
                _blockGravityService.ApplyGravity(blockMovementResult);
                
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
        
        public async Task SynchronizeBlockViews()
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
            
            var tasks = new List<Task>();
        
            foreach (var view in syncViews)
            {
                var viewConfig = _blockViewsConfigRepository.Get(view.ConfigID);
                tasks.Add(view.SpriteAnimator.PlayAndWaitAsync(viewConfig.AnimationsesConfig.DestroyAnimationConfig));
            }
            
            await Task.WhenAll(tasks);
        
            foreach (var view in syncViews)
            {
                _blockViewsRegistry.Unregister(new BlockID(view.ID));
                _spriteAnimatorsRegistry.Unregister(view.ID);
                Object.Destroy(view.gameObject);
            }
        }
    }
}