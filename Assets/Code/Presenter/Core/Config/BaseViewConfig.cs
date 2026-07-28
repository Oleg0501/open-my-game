using System;
using Code.UI.Core;
using UnityEngine;

namespace Code.Presenter.Core.Config
{
    [Serializable]
    public class BaseViewConfig
    {
        [SerializeField] private BaseView _viewPrefab;
        
        public BaseView ViewPrefab => _viewPrefab;
        public Type ViewType => _viewPrefab.GetType();
    }
}