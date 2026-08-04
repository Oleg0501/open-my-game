using Code.Presenter.Core.Config;
using UnityEngine;
using Zenject;

namespace Code.Presenter.Installers
{
    [CreateAssetMenu(fileName = "Presenter ScriptableObject Installer", menuName = "Installers/PresenterScriptableObjectInstaller")]
    public class PresenterScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UIViewsConfig _uiViewsConfig;

        public override void InstallBindings()
        {
            Container.Bind<UIViewsConfig>().FromInstance(_uiViewsConfig).AsSingle().NonLazy();
            Container.Bind<ViewsConfigRepository>().AsSingle().NonLazy();
        }
    }
}