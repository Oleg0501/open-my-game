using Zenject;

namespace Code.Scene.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BlockViewCreator>().AsSingle().NonLazy();
            Container.Bind<BlockViewsRegistry>().AsSingle().NonLazy();
            Container.Bind<InputSwipeController>().AsSingle().NonLazy();
        }
    }
}