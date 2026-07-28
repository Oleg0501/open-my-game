using System;
using System.Collections.Generic;
using Code.Presenter.Core.Interfaces;
using Code.UI.Core;

namespace Code.Presenter.Core.Implementations
{
    public class PresenterContainer : IPresenterContainer
    {
        private readonly Dictionary<Type, IPresenter> _presenters = new();
        
        public IPresenter Create<TView, TPresenter>(TView viewPrefab)
            where TView : BaseView 
            where TPresenter : IPresenter
        {
            var view = UnityEngine.Object.Instantiate(viewPrefab);
            var viewType = typeof(TView);
            var presenter = (TPresenter)Activator.CreateInstance(viewType, view);

            _presenters.Add(viewType, presenter);
            
            return presenter;
        }

        public bool Destroy<TPresenter>() where TPresenter : IPresenter
        {
            var type = typeof(TPresenter);
            
            if (!TryGetPresenter(type, out var presenter))
            {
                return false;
            }
            
            UnityEngine.Object.Destroy(presenter.View.gameObject);
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