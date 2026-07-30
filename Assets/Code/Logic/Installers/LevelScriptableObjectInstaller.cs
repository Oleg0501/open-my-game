using Code.Logic.LevelGrid;
using UnityEngine;
using Zenject;

namespace Code.Logic.Installers
{
    [CreateAssetMenu(fileName = "LevelScriptableObjectInstaller", menuName = "Installers/LevelScriptableObjectInstaller")]
    public class LevelScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelConfig _levelConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        }
    }
}