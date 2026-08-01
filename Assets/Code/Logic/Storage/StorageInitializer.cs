using Code.Logic.Storage.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Logic.Storage
{
    public class StorageInitializer : MonoBehaviour
    {
        private IStorageService _storageService;
        
        [Inject]
        public void Initialize(DiContainer diContainer, IStorageService storageService)
        {
            _storageService = storageService;
            _storageService.Initialize();
        }

        public void OnApplicationQuit()
        {
            _storageService.SaveAll();
        }
    }
}