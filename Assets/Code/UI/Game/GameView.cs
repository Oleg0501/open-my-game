using Code.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Game
{
    public class GameView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextButton;
        
        public Button.ButtonClickedEvent OnRestartButtonClicked => _restartButton.onClick;
        public Button.ButtonClickedEvent OnNextButtonClicked => _nextButton.onClick;

        public void SetLevelText(string text)
        {
            _levelText.text = text;
        }
    }
}