using Code.Logic.LevelBlock;
using Code.Logic.LevelGrid;
using Zenject;

namespace Code.Logic.Installers
{
    public class ModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridModel>().AsSingle().NonLazy();
            Container.Bind<LevelModel>().AsSingle().NonLazy();
            Container.Bind<BlockIDGenerator>().AsSingle().NonLazy();
            Container.Bind<BlockMovementService>().AsSingle().NonLazy();
            Container.Bind<BlockRegistry>().AsSingle().NonLazy();
        }
    }
}