using UnityEngine;

namespace Game.Utils
{
    
    public class CoroutineHandler : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

    }
}
