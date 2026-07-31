using Code.Scene.Implementations;
using Zenject;

namespace Code.Scene.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlockViewCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewsRegistry>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewSwipeController>().AsSingle().NonLazy();
        }
    }
}