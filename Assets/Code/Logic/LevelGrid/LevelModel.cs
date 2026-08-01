using Code.Logic.LevelGrid.Config;
using Code.Logic.Storage.Implementations;
using Zenject;

namespace Code.Logic.LevelGrid
{
    public class LevelModel : BaseJsonStorage<LevelData>
    {
        public override string SaveKey => "LevelStorage";
        public GridConfig CurrentGridConfig { get; private set; }
        public int CurrentLevel { get; private set; }

        private readonly LevelConfig _config;
        private readonly int _configsLength;
        private int _configIndex;
        
        [Inject]
        public LevelModel(LevelConfig config)
        {
            _config = config;
            _configsLength = config.GridConfigs.Length;
        }
        
        public GridConfig NextLevelConfig()
        {
            CurrentGridConfig = _config.GridConfigs[_configIndex];
            CurrentLevel = _configIndex + 1;
            
            if (_configIndex >= _configsLength - 1)
            {
                _configIndex = 0;
            }
            else
            {
                _configIndex++;
            }
            
            return CurrentGridConfig;
        }

        public override void Load()
        {
            base.Load();

            if (IsLoadDefault)
            {
                return;
            }
            
            _configIndex = Data.Level - 1;
        }
        
        public override void Save()
        {
            Data.Level = CurrentLevel;
            base.Save();
        }
        
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            _configIndex = 0;
        }
    }
}