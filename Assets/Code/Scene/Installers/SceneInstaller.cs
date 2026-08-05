using Code.Scene.Balloon;
using Code.Scene.Block.Implementations;
using Code.Scene.Core;
using Code.Scene.Core.Implementations;
using Code.Scene.SpriteAnimator;
using UnityEngine;
using Zenject;

namespace Code.Scene.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteAnimatorSystem _spriteAnimatorSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().WithId(typeof(CameraInjectID)).FromInstance(_camera).AsSingle().NonLazy();
            Container.Bind<SpriteAnimatorSystem>().FromInstance(_spriteAnimatorSystem).AsSingle().NonLazy();
            
            Container.Bind<BlockViewController>().AsSingle().NonLazy();
            Container.Bind<BalloonViewController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<CancellationTokenService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TickableRegistry>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<BlockViewsRegistry>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewSwipeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewMatchController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewsScaleService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewLayerService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BlockViewMovementService>().AsSingle().NonLazy();
        }
    }
}