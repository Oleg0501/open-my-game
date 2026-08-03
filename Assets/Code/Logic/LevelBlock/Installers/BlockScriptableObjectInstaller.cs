using Code.Logic.LevelBlock.Config;
using UnityEngine;
using Zenject;

namespace Code.Logic.LevelBlock.Installers
{
    [CreateAssetMenu(fileName = "BlockScriptableObjectInstaller", menuName = "Installers/BlockScriptableObjectInstaller")]
    public class BlockScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BlockMatchesConfig _matchesConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<BlockMatchesConfig>().FromInstance(_matchesConfig).AsSingle().NonLazy();
        }
    }
}