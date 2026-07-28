using Code.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Menu
{
    public class MenuView : BaseView
    {
        [SerializeField] private Button _playButton;
        
        public Button.ButtonClickedEvent OnPlayButtonClicked => _playButton.onClick;
    }
}