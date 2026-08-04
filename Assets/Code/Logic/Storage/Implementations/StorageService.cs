using System;
using System.Collections.Generic;
using Code.Logic.Grids;
using Code.Logic.Storage.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Logic.Storage.Implementations
{
    public class StorageService : IStorageService
    {
        private readonly DiContainer _diContainer;
        private readonly Dictionary<Type, IStorage> _storages = new();
        
        private bool _isInitialized;

        [Inject]
        public StorageService(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            
            RegisterStorages();
            LoadAll();

            _isInitialized = true;
        }

        public void SaveAll()
        {
            if (!_isInitialized)
            {
                return;
            }

            foreach (var storage in _storages.Values)
            {
                storage.Save();
            }

            PlayerPrefs.Save();
            Debug.Log("StorageService, all save storages were saved");
        }
        
        private void LoadAll()
        {
            foreach (var storage in _storages.Values)
            {
                storage.Load();
            }

            Debug.Log("StorageService, all save storages were loaded");
        }
        
        private void RegisterStorages()
        {
            var gridModel = _diContainer.Resolve<GridModel>();
            var levelModel = _diContainer.Resolve<LevelModel>();

            _storages.Add(gridModel.GetType(), gridModel);
            _storages.Add(levelModel.GetType(), levelModel);
        }
    }
}