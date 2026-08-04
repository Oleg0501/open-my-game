using System.Collections.Generic;
using Code.Logic.Blocks.Config;
using Code.Logic.Blocks.Contracts;
using Code.Logic.Grids;
using Zenject;

namespace Code.Logic.Blocks.Implementations
{
    public class BlockMatchFinder : IBlockMatchFinder
    {
        private readonly BlockMatchesConfig _matchesConfig;
        private readonly GridModel _gridModel;
        
        [Inject]
        public BlockMatchFinder(BlockMatchesConfig matchesConfig, GridModel gridModel)
        {
            _matchesConfig = matchesConfig;
            _gridModel = gridModel;
        }

        public IReadOnlyCollection<Block> FindMatches()
        {
            var matches = new HashSet<Block>();

            foreach (var matchConfig in _matchesConfig.MatchConfigs)
            {
                FindMatch(matchConfig, matches);
            }

            return matches;
        }

        private void FindMatch(BlockMatchConfig matchConfig, HashSet<Block> matches)
        {
            for (var x = 0; x < _gridModel.Width; x++)
            {
                for (var y = 0; y < _gridModel.Height; y++)
                {
                    TryMatchByConfig(matchConfig, x, y, matches);
                }
            }
        }

        private void TryMatchByConfig(BlockMatchConfig matchConfig, int startX, int startY, HashSet<Block> matches)
        {
            Block firstBlock = null;

            foreach (var matchPoint in matchConfig.MatchPoints)
            {
                var cell = _gridModel.GetCellOrNull(startX + matchPoint.x, startY + matchPoint.y);

                if (cell == null || cell.IsEmpty)
                {
                    return;
                }

                if (firstBlock == null)
                {
                    firstBlock = cell.Block;
                    
                    continue;
                }

                if (cell.Block.ConfigID != firstBlock.ConfigID)
                {
                    return;
                }
            }

            foreach (var matchPoint in matchConfig.MatchPoints)
            {
                var matchCell = _gridModel.GetCellOrNull(startX + matchPoint.x, startY + matchPoint.y);
                
                matches.Add(matchCell.Block);
            }
        }
    }
}