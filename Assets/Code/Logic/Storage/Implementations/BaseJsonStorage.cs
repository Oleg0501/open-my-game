using Code.Logic.Storage.Contracts;
using UnityEngine;

namespace Code.Logic.Storage.Implementations
{
    public abstract class BaseJsonStorage<T> : IStorage where T : new()
    {
        protected abstract string SaveKey { get; }
        
        protected T Data = new();
        protected bool IsLoadDefault;
        
        public virtual void Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
            {
                SetDefaultValues();
                IsLoadDefault = true;
                
                return;
            }

            var json = PlayerPrefs.GetString(SaveKey);
            Data = JsonUtility.FromJson<T>(json);

            if (Data == null)
            {
                SetDefaultValues();
            }
        }

        public virtual void Save()
        {
            var json = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString(SaveKey, json);
        }

        protected virtual void SetDefaultValues()
        {
        }
    }
}