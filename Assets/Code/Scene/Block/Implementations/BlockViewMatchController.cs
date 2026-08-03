using System.Threading.Tasks;
using Code.Logic.LevelBlock;
using Code.Logic.LevelBlock.Contracts;
using Code.Scene.Block.Contracts;
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

        [Inject]
        public BlockViewMatchController(IBlockGravityService blockGravityService, IBlockViewMovementService blockViewMovementService, 
            IBlockMatchFinder blockMatchFinder, IBlockMatchDestroyer blockMatchDestroyer, 
            IBlockRegistry blockRegistry, IBlockViewsRegistry blockViewsRegistry)
        {
            _blockGravityService = blockGravityService;
            _blockViewMovementService = blockViewMovementService;
            _blockMatchFinder = blockMatchFinder;
            _blockMatchDestroyer = blockMatchDestroyer;
            _blockRegistry = blockRegistry;
            _blockViewsRegistry = blockViewsRegistry;
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
                SynchronizeBlockViews();
            }
        }
        
        public void SynchronizeBlockViews()
        {
            var views = _blockViewsRegistry.GetViewsAll();
            
            foreach (var view in views)
            {
                var blockID = new BlockID(view.ID);

                if (_blockRegistry.Contains(blockID))
                {
                    continue;
                }

                _blockViewsRegistry.Unregister(blockID);
                Object.Destroy(view.gameObject);
            }
        }
    }
}