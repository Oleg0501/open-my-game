using System;
using System.Collections.Generic;
using Code.UI.Core;
using Zenject;

namespace Code.Presenter.Core.Config
{
    public class ViewsConfigRepository
    {
        private readonly Dictionary<Type, UIView> _views = new();
        
        [Inject]
        public ViewsConfigRepository(UIViewsConfig config)
        {
            Initialize(config);
        }

        private void Initialize(UIViewsConfig config)
        {
            _views.Clear();

            foreach (var viewConfig in config.ViewsConfig)
            {
                var type = viewConfig.ViewType;

                if (!_views.TryAdd(type, viewConfig.ViewPrefab))
                {
                    throw new Exception($"View with type '{type.Name}' already registered");
                }
            }
        }

        public TView GetPrefab<TView>() where TView : UIView
        {
            var viewType = typeof(TView);
            
            if (_views.TryGetValue(viewType, out var viewPrefab))
            {
                return (TView)viewPrefab;
            }
            
            throw new Exception($"View prefab of type '{viewType.Name}' not registered");
        }
    }
}