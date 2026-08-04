using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Logic.Core
{
    public abstract class BaseRegistry<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _items = new();

        protected TValue RegisterInternal(TKey key, TValue value)
        {
            if (!_items.TryAdd(key, value))
            {
                throw new Exception($"{typeof(TValue).Name} with key '{key}' already registered");
            }
            
            return value;
        }

        protected TValue UnregisterInternal(TKey key)
        {
            if (!_items.Remove(key, out var value))
            {
                throw new Exception($"{typeof(TValue).Name} with key '{key}' not registered");
            }

            return value;
        }

        protected TValue GetInternal(TKey key)
        {
            if (_items.TryGetValue(key, out var value))
            {
                return value;
            }

            throw new Exception($"{typeof(TValue).Name} with key '{key}' not registered");
        }

        protected bool ContainsInternal(TKey key)
        {
            return _items.ContainsKey(key);
        }

        protected TValue[] GetAllInternal()
        {
            return _items.Values.ToArray();
        }

        protected void ClearInternal()
        {
            _items.Clear();
        }
    }
}