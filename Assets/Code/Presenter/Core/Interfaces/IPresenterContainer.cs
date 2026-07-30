using Code.UI.Core;

namespace Code.Presenter.Core.Interfaces
{
    public interface IPresenterContainer
    {
        public IPresenter Create<TView, TPresenter>(TView view) 
            where TView : UIView 
            where TPresenter : IPresenter;
        
        public bool Destroy<TPresenter>() where TPresenter : IPresenter;
        public bool Enable<TPresenter>() where TPresenter : IPresenter;
        public bool Disable<TPresenter>() where TPresenter : IPresenter;
    }
}