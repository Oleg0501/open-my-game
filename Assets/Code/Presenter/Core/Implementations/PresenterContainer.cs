using System;
using System.Collections.Generic;
using Code.Presenter.Core.Interfaces;
using Code.UI.Core;
using Zenject;

namespace Code.Presenter.Core.Implementations
{
    public class PresenterContainer : IPresenterContainer
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<Type, IPresenter> _presenters = new();

        [Inject]
        public PresenterContainer(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public IPresenter Create<TView, TPresenter>(TView viewPrefab)
            where TView : UIView 
            where TPresenter : IPresenter
        {
            var presenterType = typeof(TPresenter);
            var presenter = _diContainer.Instantiate<TPresenter>();
            presenter.CreateView(_diContainer, viewPrefab);
            _presenters.Add(presenterType, presenter);
            
            return presenter;
        }

        public bool Destroy<TPresenter>() where TPresenter : IPresenter
        {
            var type = typeof(TPresenter);
            
            if (!TryGetPresenter(type, out var presenter))
            {
                return false;
            }
            
            presenter.DestroyView();
            _presenters.Remove(type);
            
            return true;
        }
        
        public bool Enable<TPresenter>() where TPresenter : IPresenter
        {
            var type = typeof(TPresenter);

            if (!TryGetPresenter(type, out var presenter))
            {
                return false;
            }
            
            presenter.Enable();
            
            return true;
        }
        
        public bool Disable<TPresenter>() where TPresenter : IPresenter
        {
            var type = typeof(TPresenter);
            
            if (!TryGetPresenter(type, out var presenter))
            {
                return false;
            }
            
            presenter.Disable();

            return true;
        }
        
        private bool TryGetPresenter(Type type, out IPresenter presenter)
        {
            return _presenters.TryGetValue(type, out presenter);
        }
    }
}