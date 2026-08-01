using Code.Logic.LevelGrid;
using Code.Presenter.Core.Implementations;
using Code.UI.Game;
using Zenject;

namespace Code.Presenter.Game
{
    public class GamePresenter : BasePresenter<GameView>
    {
        private readonly GridModel _gridModel;
        private readonly LevelModel _levelModel;

        [Inject]
        public GamePresenter(GridModel gridModel, LevelModel levelModel)
        {
            _gridModel = gridModel;
            _levelModel = levelModel;
        }
        
        public override void Enable()
        {
            base.Enable();
            
            _gridModel.Generate(_levelModel.NextLevelConfig());
            
            View.SetLevelText($"Level: {_levelModel.CurrentLevel}");
            View.OnRestartButtonClicked.AddListener(OnRestartButtonClicked);
            View.OnNextButtonClicked.AddListener(OnNextButtonClicked);
        }

        public override void Disable()
        {
            base.Disable();
            
            View.OnRestartButtonClicked.RemoveListener(OnRestartButtonClicked);
            View.OnNextButtonClicked.RemoveListener(OnNextButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            _gridModel.Generate(_levelModel.CurrentGridConfig);
        }
        
        private void OnNextButtonClicked()
        {
            _gridModel.Generate(_levelModel.NextLevelConfig());
            View.SetLevelText($"Level: {_levelModel.CurrentLevel}");
        }
    }
}