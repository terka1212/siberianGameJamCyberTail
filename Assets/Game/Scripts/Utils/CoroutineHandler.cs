using UnityEngine;

namespace Game.Utils
{
    public class CoroutineHandler : MonoBehaviour
    {
        public static CoroutineHandler instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}