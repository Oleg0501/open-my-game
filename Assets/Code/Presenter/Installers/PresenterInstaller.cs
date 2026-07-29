using Code.Presenter.Core.Implementations;
using Zenject;

namespace Code.Presenter.Installers
{
    public class PresenterInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PresenterContainer>().AsSingle();
        }
    }
}