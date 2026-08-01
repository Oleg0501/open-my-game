using Code.Logic.Storage.Implementations;
using Zenject;

namespace Code.Logic.Storage.Installers
{
    public class StorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<StorageService>().AsSingle().NonLazy();
        }
    }
}