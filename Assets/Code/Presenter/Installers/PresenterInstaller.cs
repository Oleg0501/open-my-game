using Code.Presenter.Core.Config;
using Code.Presenter.Core.Implementations;
using UnityEngine;
using Zenject;

namespace Code.Presenter.Installers
{
    public class PresenterInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        
        public override void InstallBindings()
        {
            Container.Bind<Canvas>().WithId(typeof(CanvasInjectID)).FromInstance(_canvas).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PresenterContainer>().AsSingle().NonLazy();
        }
    }
}