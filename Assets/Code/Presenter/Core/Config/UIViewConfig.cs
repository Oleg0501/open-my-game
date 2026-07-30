using System;
using Code.UI.Core;
using UnityEngine;

namespace Code.Presenter.Core.Config
{
    [Serializable]
    public class UIViewConfig
    {
        [SerializeField] private UIView _viewPrefab;
        
        public UIView ViewPrefab => _viewPrefab;
        public Type ViewType => _viewPrefab.GetType();
    }
}