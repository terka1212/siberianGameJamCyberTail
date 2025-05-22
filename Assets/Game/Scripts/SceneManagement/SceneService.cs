using System.Collections;
using Game.Data;
using Game.Events;
using Game.Utils;
using VContainer;

namespace Game.SceneManagement
{
    public class SceneService
    {
        private SceneData _sceneData;
        
        private EventManager _eventManager;

        [Inject]
        public SceneService(SceneData sceneData, EventManager eventManager)
        {
            _sceneData = sceneData;
            _eventManager = eventManager;
        }
        
        public IEnumerator LoadScene(SceneName sceneLoadTo) => SceneLoader.LoadScene(sceneLoadTo);

        public void SaveLoadFromScene(SceneName sceneName)
        {
            _sceneData.sceneLoadedFrom = sceneName;
        }

        public void InvokeStartSceneTransition(SceneName sceneLoadFrom, SceneName sceneLoadTo)
        {
            _eventManager.InvokeOnStartSceneTransition(sceneLoadTo, sceneLoadFrom);
        }
        
        public void InvokeEndSceneTransition(SceneName sceneLoadFrom, SceneName sceneLoadTo)
        {
            _eventManager.InvokeOnEndSceneTransition(sceneLoadTo, sceneLoadFrom);
        }
    }
}