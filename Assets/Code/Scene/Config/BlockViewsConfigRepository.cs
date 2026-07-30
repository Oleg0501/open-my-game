using System;
using System.Collections.Generic;
using Zenject;

namespace Code.Scene.Config
{
    public class BlockViewsConfigRepository
    {
        private readonly Dictionary<string, BlockViewConfig> _views = new();
        
        [Inject]
        public BlockViewsConfigRepository(BlockViewsConfig config)
        {
            Initialize(config);
        }

        private void Initialize(BlockViewsConfig config)
        {
            _views.Clear();

            foreach (var viewConfig in config.ViewConfigs)
            {
                var id = viewConfig.IDConfig.ID;
                
                if (!_views.TryAdd(id, viewConfig))
                {
                    throw new Exception($"Block view config with ID '{id}' already registered");
                }
            }
        }

        public BlockViewConfig Get(string id)
        {
            if (_views.TryGetValue(id, out var viewPrefab))
            {
                return viewPrefab;
            }
            
            throw new Exception($"Block view config with ID '{id}' not registered");
        }
    }
}