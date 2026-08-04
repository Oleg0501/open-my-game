using UnityEngine;

namespace Code.Presenter.Core.Config
{
    [CreateAssetMenu(menuName = "UI/Canvas Config", fileName = "CanvasConfig")]
    public class CanvasConfig : ScriptableObject
    {
        public static string CanvasID;
        
        [SerializeField] private string _canvasID;

        private void OnEnable()
        {
            CanvasID = _canvasID;
        }
    }
}