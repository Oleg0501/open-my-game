using Code.Logic.Blocks.Implementations;
using Zenject;

namespace Code.Logic.Blocks.Installers
{
    public class BlockInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlockIDGenerator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockRegistry>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockMovementService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockDropService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockMatchFinder>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockMatchDestroyer>().AsSingle().NonLazy();
        }
    }
}