using System.Linq;
using Code.Logic.Core;
using Zenject;

namespace Code.Scene.Block.Config
{
    public sealed class BlockViewsConfigRepository : BaseRepository<string, BlockViewConfig>
    {
        [Inject]
        public BlockViewsConfigRepository(BlockViewsConfig config)
        {
            Initialize(config.ViewConfigs.Select(x => (x.IDConfig.ID, x)));
        }

        public BlockViewConfig Get(string id)
        {
            return GetInternal(id);
        }
    }
}