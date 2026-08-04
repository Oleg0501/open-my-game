using Code.Logic.Blocks.Config;
using UnityEngine;
using Zenject;

namespace Code.Logic.Blocks.Installers
{
    [CreateAssetMenu(fileName = "Block ScriptableObject Installer", menuName = "Installers/BlockScriptableObjectInstaller")]
    public class BlockScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BlockMatchesConfig _matchesConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<BlockMatchesConfig>().FromInstance(_matchesConfig).AsSingle().NonLazy();
        }
    }
}