using Code.Logic.Grids.Config;
using Code.Logic.Storage.Implementations;
using Zenject;

namespace Code.Logic.Grids
{
    public sealed class LevelModel : BaseJsonStorage<LevelData>
    {
        protected override string SaveKey => "LevelStorage";
        
        public GridConfig CurrentGridConfig { get; private set; }
        public int CurrentLevel { get; private set; }

        private readonly GridsConfig _gridsConfig;
        private readonly int _gridConfigsCount;
        
        private int _gridConfigIndex;
        
        [Inject]
        public LevelModel(GridsConfig gridsConfig)
        {
            _gridsConfig = gridsConfig;
            _gridConfigsCount = gridsConfig.Configs.Length;
        }
        
        public GridConfig NextGrid()
        {
            CurrentGridConfig = _gridsConfig.Configs[_gridConfigIndex];
            CurrentLevel = _gridConfigIndex + 1;
            
            if (_gridConfigIndex >= _gridConfigsCount - 1)
            {
                _gridConfigIndex = 0;
            }
            else
            {
                _gridConfigIndex++;
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
            
            _gridConfigIndex = Data.Level - 1;
        }
        
        public override void Save()
        {
            Data.Level = CurrentLevel;
            base.Save();
        }
        
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();
            _gridConfigIndex = 0;
        }
    }
}