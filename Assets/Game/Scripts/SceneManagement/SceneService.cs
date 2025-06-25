using System.Collections;
using Game.Data;
using Game.Events;
using Game.GameObjects;
using Game.UI;
using Game.Utils;
using VContainer;

namespace Game.SceneManagement
{
    public class SceneService
    {
        private readonly FadeService _fadeService;
        private readonly CoroutineHandler _coroutineHandler;
        private readonly EventManager _eventManager;
        private readonly SceneData _sceneData;

        [Inject]
        public SceneService(FadeService fadeService,
            CoroutineHandler coroutineHandler, EventManager eventManager, SceneData sceneData)
        {
            _fadeService = fadeService;
            _coroutineHandler = coroutineHandler;
            _eventManager = eventManager;
            _sceneData = sceneData;
        }

        public IEnumerator SceneTransition(SceneName sceneLoadFrom, SceneName sceneLoadTo)
        {
            _eventManager.InvokeOnStartSceneTransitionEvent(sceneLoadFrom, sceneLoadTo);
            _sceneData.sceneLoadedFrom = sceneLoadFrom;

            yield return _coroutineHandler.StartCoroutine(_fadeService.Show());
            yield return _coroutineHandler.StartCoroutine(SceneLoader.LoadScene(sceneLoadTo));
            yield return _coroutineHandler.StartCoroutine(_fadeService.Hide());
            _eventManager.InvokeOnEndSceneTransitionEvent(sceneLoadFrom, sceneLoadTo);
        }

        public SceneName GetCurrentScene()
        {
            return SceneLoader.GetCurrentSceneName();
        }
    }
}