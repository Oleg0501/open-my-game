using Code.Logic.Grids.Config;
using UnityEngine;
using Zenject;

namespace Code.Logic.Grids.Installers
{
    [CreateAssetMenu(fileName = "Grid ScriptableObject Installer", menuName = "Installers/GridScriptableObjectInstaller")]
    public class GridScriptableObjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GridsConfig gridsConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<GridsConfig>().FromInstance(gridsConfig).AsSingle().NonLazy();
        }
    }
}