using System.Collections;
using Game.Data;
using Game.Navigation;
using Game.UI;
using Game.Utils;
using VContainer;

namespace Game.SceneManagement
{
    public class ScenePresenter
    {
        private readonly PointAndClickService _pointAndClickService;
        private readonly FadeService _fadeService;
        private readonly SceneService _sceneService;
        private readonly CoroutineHandler _coroutineHandler;

        [Inject]
        public ScenePresenter(PointAndClickService pointAndClickService, FadeService fadeService,
            SceneService sceneService, CoroutineHandler coroutineHandler)
        {
            _pointAndClickService = pointAndClickService;
            _fadeService = fadeService;
            _sceneService = sceneService;
            _coroutineHandler = coroutineHandler;
        }

        public void InvokeTransition(SceneName sceneFrom, SceneName sceneTo)
        {
            _coroutineHandler.StartCoroutine(SceneTransition(sceneFrom, sceneTo));
        }
        

        private IEnumerator SceneTransition(SceneName sceneLoadFrom, SceneName sceneLoadTo)
        {
            _sceneService.InvokeStartSceneTransition(sceneLoadFrom, sceneLoadTo);
            _sceneService.SaveLoadFromScene(sceneLoadFrom);

            _pointAndClickService.Block();

            yield return _coroutineHandler.StartCoroutine(_fadeService.Show());
            yield return _coroutineHandler.StartCoroutine(_sceneService.LoadScene(sceneLoadTo));
            yield return _coroutineHandler.StartCoroutine(_fadeService.Hide());

            _pointAndClickService.Unblock();
            
            _sceneService.InvokeEndSceneTransition(sceneLoadFrom, sceneLoadTo);
        }
    }
}