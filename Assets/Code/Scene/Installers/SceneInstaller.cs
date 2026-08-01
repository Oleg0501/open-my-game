using Code.Scene.Balloon.Implementations;
using Code.Scene.Block.Implementations;
using Code.Scene.Implementations;
using Code.Scene.SpriteAnimator;
using Code.Scene.SpriteAnimator.Implementations;
using UnityEngine;
using Zenject;

namespace Code.Scene.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private SpriteAnimatorSystem _spriteAnimatorSystem;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlockViewCreator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewsRegistry>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewSwipeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewLayerService>().AsSingle().NonLazy();
            Container.Bind<BalloonViewController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteAnimatorsRegistry>().AsSingle().NonLazy();
            
            Container.Bind<Camera>().WithId("Camera").FromInstance(_camera).AsSingle().NonLazy();
            Container.Bind<Canvas>().WithId("Canvas").FromInstance(_canvas).AsSingle().NonLazy();
            Container.Bind<SpriteAnimatorSystem>().FromInstance(_spriteAnimatorSystem).AsSingle().NonLazy();
        }
    }
}