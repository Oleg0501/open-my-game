using UnityEngine;

namespace Code.Presenter.Core.Config
{
    [CreateAssetMenu(menuName = "UI/UI Views Config", fileName = "UIViewsConfig")]
    public class UIViewsConfig : ScriptableObject
    {
        [SerializeField]
        private UIViewConfig[] _viewsConfig;

        public UIViewConfig[] ViewsConfig => _viewsConfig;
    }
}