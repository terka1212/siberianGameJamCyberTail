using UnityEngine;

namespace Game.DebugUtilities
{
    public class SceneDebugObject : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(this.gameObject);
        }
    }
}