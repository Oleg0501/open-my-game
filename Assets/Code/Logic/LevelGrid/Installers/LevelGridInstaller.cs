using Zenject;

namespace Code.Logic.LevelGrid.Installers
{
    public class LevelGridInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridModel>().AsSingle().NonLazy();
            Container.Bind<LevelModel>().AsSingle().NonLazy();
        }
    }
}