using Code.Scene.Implementations;
using UnityEngine;
using Zenject;

namespace Code.Scene.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private SpriteAnimatorSystem _spriteAnimatorSystem;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlockViewCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewsRegistry>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewSwipeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewLayerService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteAnimatorsRegistry>().AsSingle().NonLazy();
            Container.Bind<SpriteAnimatorSystem>().FromInstance(_spriteAnimatorSystem).AsSingle().NonLazy();
        }
    }
}