using Game.Data;
using Game.SceneManagement;
using Game.Utils;
using UnityEngine;

namespace Game.UI
{
    public class LoadLevelButton : MonoBehaviour
    {
        [SerializeField] private SceneName _sceneName;
        
        public void Click()
        {
            StartCoroutine(SceneLoader.LoadScene(_sceneName));
        }
    }
}