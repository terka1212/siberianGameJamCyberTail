using Game.UI;
using Game.Utils;
using UnityEngine;

namespace Game.SceneManagement
{
    public class NextSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneName nextSceneName;
        [SerializeField] private Fade fadeScreen;
        [SerializeField] private CoroutineHandler _coroutineHandler;
        private bool isSceneLoading;
        
        private void Awake()
        {
            isSceneLoading = false;
        }

        public void NextScene()
        {
            if (isSceneLoading) return;
            
            StartCoroutine(SceneLoader.LoadScene(_coroutineHandler, fadeScreen, nextSceneName));
            isSceneLoading = true;
        }
    }
}