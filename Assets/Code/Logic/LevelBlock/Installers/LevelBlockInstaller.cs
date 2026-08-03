using Code.Logic.LevelBlock.Implementations;
using Zenject;

namespace Code.Logic.LevelBlock.Installers
{
    public class LevelBlockInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlockIDGenerator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockMovementService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockGravityService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockMatchFinder>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockMatchDestroyer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockRegistry>().AsSingle().NonLazy();
        }
    }
}