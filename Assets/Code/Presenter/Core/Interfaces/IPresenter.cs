using Code.UI.Core;

namespace Code.Presenter.Core.Interfaces
{
    public interface IPresenter
    {
        BaseView View { get; }
        void Enable();
        void Disable();
    }
}