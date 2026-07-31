using Code.Logic.LevelGrid.Config;
using Zenject;

namespace Code.Logic.LevelGrid
{
    public class LevelModel
    {
        public GridConfig CurrentGridConfig { get; private set; }
        
        private readonly LevelConfig _levelConfig;
        private readonly int _levelsLength;
        
        private int _currentLevel = -1;

        [Inject]
        public LevelModel(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _levelsLength = levelConfig.GridConfigs.Length;
        }
        
        public GridConfig NextLevelConfig()
        {
            if (_currentLevel >= _levelsLength - 1)
            {
                _currentLevel = 0;
            }
            else
            {
                _currentLevel++;
            }
            
            CurrentGridConfig = _levelConfig.GridConfigs[_currentLevel];
            
            return CurrentGridConfig;
        }
    }
}