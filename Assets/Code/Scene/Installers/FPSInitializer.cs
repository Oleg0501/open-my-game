using UnityEngine;

namespace Code.Scene.Installers
{
    public class FPSInitializer : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;
        
        private void Awake()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = _targetFrameRate;
#endif
        }
    }
}