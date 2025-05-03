using System.Collections;
using Game.UI;
using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
    public static class SceneLoader
    {
        public static IEnumerator LoadScene(CoroutineHandler coroutineHandler, Fade screenFade, SceneName sceneName)
        {
            //yield return coroutineHandler.StartCoroutine(screenFade.FadeIn());
            
            var scene = SceneManager.LoadSceneAsync(sceneName.ToString());
            if (scene is not null)
            {
                scene.allowSceneActivation = false;
            }
            else
            {
                Debug.LogWarning($"Scene {sceneName} is not loaded");
                yield break;
            }
            
            do
            {
                yield return null;
            } while (scene.progress < 0.9f);
            scene.allowSceneActivation = true;
        }
    }
    
    public enum SceneName
    {
        SampleScene,
        SampleScene2
    }
}