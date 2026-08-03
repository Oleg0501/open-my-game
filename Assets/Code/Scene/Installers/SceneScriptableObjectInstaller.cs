using Code.Scene.Balloon.Config;
using Code.Scene.Config;
using UnityEngine;
using Zenject;

namespace Code.Scene.Installers
{
    [CreateAssetMenu(fileName = "SceneScriptableObjectInstaller", menuName = "Installers/SceneScriptableObjectInstaller")]
    public class SceneScriptableObjectInstaller : ScriptableObjectInstaller<SceneScriptableObjectInstaller>
    {
        [SerializeField] private InputSwipeConfig _inputSwipeConfig;
        [SerializeField] private BlockViewsConfig _blockViewsConfig;
        [SerializeField] private BalloonViewsConfig _balloonViewsConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<InputSwipeConfig>().FromInstance(_inputSwipeConfig).AsSingle().NonLazy();
            Container.Bind<BlockViewsConfig>().FromInstance(_blockViewsConfig).AsSingle().NonLazy();
            Container.Bind<BalloonViewsConfig>().FromInstance(_balloonViewsConfig).AsSingle().NonLazy();
            Container.Bind<BlockViewsConfigRepository>().AsSingle().NonLazy();
        }
    }
}