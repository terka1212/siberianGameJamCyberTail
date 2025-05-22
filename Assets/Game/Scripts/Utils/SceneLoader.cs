using System.Collections;
using Game.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Utils
{
    public static class SceneLoader
    {
        public static IEnumerator LoadScene(SceneName sceneName)
        {
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
}