using Code.Presenter.Core.Config;
using UnityEngine;
using Zenject;

namespace Code.Presenter.Installers
{
    [CreateAssetMenu(fileName = "PresenterScriptableObjectInstaller", menuName = "Installers/PresenterScriptableObjectInstaller")]
    public class PresenterScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UIViewsConfig uiViewsConfig;

        public override void InstallBindings()
        {
            Container.Bind<UIViewsConfig>().FromInstance(uiViewsConfig).AsSingle().NonLazy();
            Container.Bind<ViewsConfigRepository>().AsSingle().NonLazy();
        }
    }
}