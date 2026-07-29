using Code.Presenter.Core.Config;
using Code.Presenter.Core.Interfaces;
using Code.Presenter.Game;
using Code.Presenter.Menu;
using Code.UI.Game;
using Code.UI.Menu;
using UnityEngine;
using Zenject;

namespace Code.Presenter.Installers
{
    public class PresenterInitializer : MonoBehaviour
    {
        [Inject]
        public void Initialize(ViewsConfigRepository viewsConfigRepository, IPresenterContainer presenterContainer)
        {
            var menuViewPrefab = viewsConfigRepository.GetPrefab<MenuView>();
            var menuPresenter = presenterContainer.Create<MenuView, MenuPresenter>(menuViewPrefab);
            
            var gameViewPrefab = viewsConfigRepository.GetPrefab<GameView>();
            presenterContainer.Create<GameView, GamePresenter>(gameViewPrefab);
            
            menuPresenter.Enable();
        }
    }
}