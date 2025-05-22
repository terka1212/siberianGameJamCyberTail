using System;
using Game.Data;

namespace Game.Events
{
    public class EventManager
    {
        //TODO удалить 4 строки ниже
        public static event Action InventoryChange;

        public static event Action StartSceneLoading;

        public static void InvokeInventoryChangeEvent() => InventoryChange?.Invoke();

        public static void InvokeStartSceneLoading() => StartSceneLoading?.Invoke();

        //Scene Events
        public event Action<SceneName, SceneName> OnStartSceneTransition;

        public event Action<SceneName, SceneName> OnEndSceneTransition;
        
        //Scene Events Handling
        public void InvokeOnStartSceneTransition(SceneName sceneFrom, SceneName sceneTo) =>
            OnStartSceneTransition?.Invoke(sceneFrom, sceneTo);

        public void InvokeOnEndSceneTransition(SceneName sceneFrom, SceneName sceneTo) =>
            OnEndSceneTransition?.Invoke(sceneFrom, sceneTo);
    }
}