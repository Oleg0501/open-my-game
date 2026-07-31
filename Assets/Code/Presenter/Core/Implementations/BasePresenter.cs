using Code.Presenter.Core.Contracts;
using Code.UI.Core;
using UnityEngine;
using Zenject;

namespace Code.Presenter.Core.Implementations
{
    public abstract class BasePresenter<TView> : IPresenter where TView : UIView
    {
        protected TView View;

        [Inject(Id = "Canvas")] private Canvas _canvas;
        
        public void CreateView(DiContainer diContainer, UIView viewPrefab)
        {
            View = diContainer.InstantiatePrefabForComponent<TView>(viewPrefab, _canvas.transform);
        }
        
        public void DestroyView()
        {
            Object.Destroy(View.gameObject);
        }

        public virtual void Enable()
        {
            View.Enable();
        }

        public virtual void Disable()
        {
            View.Disable();
        }
    }
}