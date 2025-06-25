using System.Collections;
using Game.Data;
using Game.Events;
using Game.GameObjects;
using Game.UI;
using Game.Utils;
using VContainer;

namespace Game.SceneManagement
{
    public class ScenePresenter
    {
        private readonly SceneService _sceneService;
        private readonly CoroutineHandler _coroutineHandler;

        [Inject]
        public ScenePresenter(SceneService sceneService, CoroutineHandler coroutineHandler)
        {
            _sceneService = sceneService;
            _coroutineHandler = coroutineHandler;
        }

        public void InvokeTransition(SceneName sceneFrom, SceneName sceneTo)
        {
            _coroutineHandler.StartCoroutine(_sceneService.SceneTransition(sceneFrom, sceneTo));
        }

        public SceneName GetCurrentScene()
        {
            return _sceneService.GetCurrentScene();
        }
    }
}