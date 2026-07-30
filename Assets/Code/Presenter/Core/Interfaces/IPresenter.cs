using Code.UI.Core;
using Zenject;

namespace Code.Presenter.Core.Interfaces
{
    public interface IPresenter
    {
        void CreateView(DiContainer diContainer, UIView viewPrefab);
        void DestroyView();
        void Enable();
        void Disable();
    }
}