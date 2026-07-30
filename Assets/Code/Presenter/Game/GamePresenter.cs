using Code.Logic;
using Code.Logic.LevelBlock;
using Code.Presenter.Core.Implementations;
using Code.UI.Game;
using Zenject;

namespace Code.Presenter.Game
{
    public class GamePresenter : BasePresenter<GameView>
    {
        private readonly BlockGenerator _blockGenerator;

        [Inject]
        public GamePresenter(BlockGenerator blockGenerator)
        {
            _blockGenerator = blockGenerator;
        }
        
        public override void Enable()
        {
            base.Enable();
            
            _blockGenerator.StartNextLevel();
            
            View.OnRestartButtonClicked.AddListener(OnRestartButtonClicked);
            View.OnNextButtonClicked.AddListener(OnNextButtonClicked);
            View.SetLevelText("Level 1");
        }

        public override void Disable()
        {
            base.Disable();
            
            View.OnRestartButtonClicked.RemoveListener(OnRestartButtonClicked);
            View.OnNextButtonClicked.RemoveListener(OnNextButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            _blockGenerator.RestartCurrentLevel();
        }
        
        private void OnNextButtonClicked()
        {
            _blockGenerator.StartNextLevel();
        }
    }
}