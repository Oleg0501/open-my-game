using System.Collections.Generic;
using UnityEngine;

namespace Code.Presenter.Core.Config
{
    [CreateAssetMenu(menuName = "UI/Views Config", fileName = "ViewsConfig")]
    public class ViewsConfig : ScriptableObject
    {
        [SerializeField]
        private List<BaseViewConfig> _views = new();

        public IReadOnlyList<BaseViewConfig> Views => _views;
    }
}