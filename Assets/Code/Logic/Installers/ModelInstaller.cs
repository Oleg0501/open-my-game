using Code.Logic.LevelBlock;
using Code.Logic.LevelGrid;
using Zenject;

namespace Code.Logic.Installers
{
    public class ModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GridModel>().AsSingle();
            Container.Bind<LevelModel>().AsSingle();
            Container.Bind<BlockGenerator>().AsSingle();
        }
    }
}