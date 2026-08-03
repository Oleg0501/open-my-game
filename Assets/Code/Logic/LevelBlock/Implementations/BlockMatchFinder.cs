using System.Collections.Generic;
using Code.Logic.LevelBlock.Config;
using Code.Logic.LevelBlock.Contracts;
using Code.Logic.LevelGrid;
using Zenject;

namespace Code.Logic.LevelBlock.Implementations
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

        public HashSet<Block> FindMatches()
        {
            var result = new HashSet<Block>();

            foreach (var matchConfig in _matchesConfig.MatchConfigs)
            {
                FindMatch(matchConfig, result);
            }

            return result;
        }

        private void FindMatch(BlockMatchConfig matchConfig, HashSet<Block> result)
        {
            for (var x = 0; x < _gridModel.Width; x++)
            {
                for (var y = 0; y < _gridModel.Height; y++)
                {
                    TryMatchByConfig(matchConfig, x, y, result);
                }
            }
        }

        private void TryMatchByConfig(BlockMatchConfig matchConfig, int startX, int startY, HashSet<Block> result)
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
                result.Add(_gridModel.GetCellOrNull(startX + matchPoint.x, startY + matchPoint.y).Block);
            }
        }
    }
}