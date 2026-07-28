using Code.Presenter.Core.Implementations;
using Code.UI.Menu;

namespace Code.Presenter.Menu
{
    public class MenuPresenter : BasePresenter<MenuView>
    {
        public MenuPresenter(MenuView view) : base(view)
        {
            
        }

        public override void Enable()
        {
            base.Enable();
            TypedView.OnPlayButtonClicked.AddListener(OnPlayButtonClicked);
        }

        public override void Disable()
        {
            base.Disable();
            TypedView.OnPlayButtonClicked.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
        }
    }
}