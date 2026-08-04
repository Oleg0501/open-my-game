using System;
using System.Collections.Generic;

namespace Code.Logic.Core
{
    public abstract class BaseRepository<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _items = new();

        protected void Initialize(IEnumerable<(TKey Key, TValue Value)> values)
        {
            _items.Clear();

            foreach (var (key, value) in values)
            {
                if (!_items.TryAdd(key, value))
                {
                    throw new Exception($"{typeof(TValue).Name} with key '{key}' already registered");
                }
            }
        }

        protected TValue GetInternal(TKey key)
        {
            if (_items.TryGetValue(key, out var value))
            {
                return value;
            }

            throw new Exception($"{typeof(TValue).Name} with key '{key}' not registered");
        }
    }
}