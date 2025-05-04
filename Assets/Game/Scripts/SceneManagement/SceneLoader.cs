using System.Collections;
using Game.Events;
using Game.UI;
using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.SceneManagement
{
    public static class SceneLoader
    {
        public static bool isSceneLoading = false;
        public static IEnumerator LoadScene(SceneName sceneName)
        {
            isSceneLoading = true;
            EventManager.InvokeStartSceneLoading();
            yield return CoroutineHandler.instance.StartCoroutine(Fade.FadeIn());
            
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
            isSceneLoading = false;
            yield return CoroutineHandler.instance.StartCoroutine(Fade.FadeOut());
        }
    }
    
    public enum SceneName
    {
        Bootstrap,
        Menu,
        Tutorial,
        Street,
        Market,
        Roof,
        SampleScene,
        SampleScene2
    }
}