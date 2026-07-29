using Code.Presenter.Core.Config;
using UnityEngine;
using Zenject;

namespace Code.Presenter.Installers
{
    [CreateAssetMenu(fileName = "PresenterScriptableObjectInstaller", menuName = "Installers/PresenterScriptableObjectInstaller")]
    public class PresenterScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ViewsConfig _viewsConfig;

        public override void InstallBindings()
        {
            Container.Bind<ViewsConfig>().FromInstance(_viewsConfig).AsSingle();
            Container.Bind<ViewsConfigRepository>().AsSingle();
        }
    }
}