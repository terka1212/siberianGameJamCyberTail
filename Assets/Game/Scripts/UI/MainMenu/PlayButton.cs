using System;
using Game.Data;
using Game.SceneManagement;
using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private SceneName _sceneName;
        
        private ScenePresenter _scenePresenter;

        [Inject]
        public void Construct(ScenePresenter scenePresenter)
        {
            _scenePresenter = scenePresenter;
        }
        
        public void Play()
        {
            _scenePresenter.InvokeTransition(_scenePresenter.GetCurrentScene(), _sceneName);
        }
    }
}