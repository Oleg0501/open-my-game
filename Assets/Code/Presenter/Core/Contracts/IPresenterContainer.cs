using Code.UI.Core;

namespace Code.Presenter.Core.Contracts
{
    public interface IPresenterContainer
    {
        IPresenter Create<TView, TPresenter>(TView view) 
            where TView : UIView 
            where TPresenter : IPresenter;
        
        bool Destroy<TPresenter>() where TPresenter : IPresenter;
        bool Enable<TPresenter>() where TPresenter : IPresenter;
        bool Disable<TPresenter>() where TPresenter : IPresenter;
    }
}