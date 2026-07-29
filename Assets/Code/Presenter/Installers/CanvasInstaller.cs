using UnityEngine;
using Zenject;

namespace Code.Presenter.Installers
{
    public class CanvasInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().WithId("Canvas").FromInstance(_canvas).AsSingle();
        }
    }
}