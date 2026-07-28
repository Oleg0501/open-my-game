using Code.Presenter.Core.Interfaces;
using Code.UI.Core;

namespace Code.Presenter.Core.Implementations
{
    public abstract class BasePresenter<TView> : IPresenter where TView : BaseView
    {
        public BaseView View => TypedView;

        protected readonly TView TypedView;

        protected BasePresenter(TView view)
        {
            TypedView = view;
        }

        public virtual void Enable()
        {
            View.Enable();
        }

        public virtual void Disable()
        {
            View.Disable();
        }
    }
}