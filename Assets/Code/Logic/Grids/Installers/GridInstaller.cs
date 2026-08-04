using Zenject;

namespace Code.Logic.Grids.Installers
{
    public class GridInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridModel>().AsSingle().NonLazy();
            Container.Bind<LevelModel>().AsSingle().NonLazy();
        }
    }
}