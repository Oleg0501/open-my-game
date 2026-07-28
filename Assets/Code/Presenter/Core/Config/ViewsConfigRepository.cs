using System;
using System.Collections.Generic;
using Code.UI.Core;

namespace Code.Presenter.Core.Config
{
    public class ViewsConfigRepository
    {
        private readonly Dictionary<Type, BaseView> _views = new();
        
        public ViewsConfigRepository(ViewsConfig config)
        {
            Initialize(config);
        }

        private void Initialize(ViewsConfig viewsConfig)
        {
            _views.Clear();

            foreach (var viewConfiguration in viewsConfig.Views)
            {
                var type = viewConfiguration.ViewType;

                if (!_views.TryAdd(type, viewConfiguration.ViewPrefab))
                {
                    throw new Exception($"View with type '{type.Name}' already registered");
                }
            }
        }

        public BaseView GetPrefab(Type viewType)
        {
            if (_views.TryGetValue(viewType, out var viewPrefab))
            {
                return viewPrefab;
            }
            
            throw new Exception($"View prefab of type '{viewType.Name}' not registered");
        }
    }
}