using System;
using System.Linq;
using Code.Logic.Core;
using Code.UI.Core;
using Zenject;

namespace Code.Presenter.Core.Config
{
    public sealed class ViewsConfigRepository : BaseRepository<Type, UIView>
    {
        [Inject]
        public ViewsConfigRepository(UIViewsConfig config)
        {
            Initialize(config.ViewsConfig.Select(x => (x.ViewType, x.ViewPrefab)));
        }

        public TView GetPrefab<TView>() where TView : UIView
        {
            return (TView)GetInternal(typeof(TView));
        }
    }
}