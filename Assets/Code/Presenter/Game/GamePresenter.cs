using Code.Presenter.Core.Implementations;
using Code.UI.Game;
using Zenject;

namespace Code.Presenter.Game
{
    public class GamePresenter : BasePresenter<GameView>
    {
        [Inject]
        public GamePresenter()
        {
        }
        
        public override void Enable()
        {
            base.Enable();
            
            View.OnRestartButtonClicked.AddListener(OnRestartButtonClicked);
            View.SetLevelText("Level 1");
        }

        public override void Disable()
        {
            base.Disable();
            
            View.OnNextButtonClicked.RemoveListener(OnNextButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
        }
        
        private void OnNextButtonClicked()
        {
        }
    }
}