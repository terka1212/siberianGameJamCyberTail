using Game.Data;
using Game.UI;
using Game.Utils;
using UnityEngine;

namespace Game.SceneManagement
{
    public class NextSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneName nextSceneName;
        private bool isSceneLoading;
        
        private void Awake()
        {
            isSceneLoading = false;
        }

        public void NextScene()
        {
            if (isSceneLoading) return;
            
            StartCoroutine(SceneLoader.LoadScene(nextSceneName));
            isSceneLoading = true;
        }
    }
}