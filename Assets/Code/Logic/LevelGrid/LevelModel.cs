using Code.Logic.LevelGrid.Config;
using Zenject;

namespace Code.Logic.LevelGrid
{
    public class LevelModel
    {
        public GridConfig CurrentGridConfig { get; private set; }
        public int CurrentLevel => _configIndex + 1;
        
        private readonly LevelConfig _levelConfig;
        private readonly int _levelsLength;
        private int _configIndex = -1;
        
        [Inject]
        public LevelModel(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _levelsLength = levelConfig.GridConfigs.Length;
        }
        
        public GridConfig NextLevelConfig()
        {
            if (_configIndex >= _levelsLength - 1)
            {
                _configIndex = 0;
            }
            else
            {
                _configIndex++;
            }
            
            CurrentGridConfig = _levelConfig.GridConfigs[_configIndex];
            
            return CurrentGridConfig;
        }
    }
}