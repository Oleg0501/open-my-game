using Code.Presenter.Core.Contracts;
using Code.Presenter.Core.Implementations;
using Code.Presenter.Game;
using Code.UI.Menu;
using Zenject;

namespace Code.Presenter.Menu
{
    public class MenuPresenter : BasePresenter<MenuView>
    {
        private readonly IPresenterContainer _presenterContainer;
        
        [Inject]
        public MenuPresenter(IPresenterContainer presenterContainer)
        {
            _presenterContainer = presenterContainer;
        }

        public override void Enable()
        {
            base.Enable();
            
            View.OnPlayButtonClicked.AddListener(OnPlayButtonClicked);
        }

        public override void Disable()
        {
            base.Disable();
            
            View.OnPlayButtonClicked.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _presenterContainer.Enable<GamePresenter>();
            _presenterContainer.Disable<MenuPresenter>();
        }
    }
}