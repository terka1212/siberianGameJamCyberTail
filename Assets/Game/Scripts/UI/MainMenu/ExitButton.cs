using UnityEngine;

namespace Game.UI
{
    public class ExitButton : MonoBehaviour
    {
        public void Quit()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}