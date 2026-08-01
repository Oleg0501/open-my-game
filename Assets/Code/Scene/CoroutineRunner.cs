using UnityEngine;

namespace Code.Scene
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}