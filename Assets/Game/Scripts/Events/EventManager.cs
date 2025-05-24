using System;
using Game.Data;
using UnityEngine.AI;

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

        //Point&Click Events
        public event Action OnPointAndClickBlocked;
        public event Action OnPointAndClickUnblocked;
        public event Action<string> OnHandleClickValidationFailed;
        public event Action<NavMeshAgent> OnDestinationReachedByPlayer;
        public event Action OnDestinationReachedByOppositeAgent;

        //Scene Events Handling
        public void InvokeOnStartSceneTransition(SceneName sceneFrom, SceneName sceneTo) =>
            OnStartSceneTransition?.Invoke(sceneFrom, sceneTo);

        public void InvokeOnEndSceneTransition(SceneName sceneFrom, SceneName sceneTo) =>
            OnEndSceneTransition?.Invoke(sceneFrom, sceneTo);

        //Point&Click Events Handling
        public void InvokeOnPointAndClickBlocked() => OnPointAndClickBlocked?.Invoke();
        public void InvokeOnPointAndClickUnblocked() => OnPointAndClickUnblocked?.Invoke();
        public void InvokeOnHandleClickValidationFailed(string msg) => OnHandleClickValidationFailed?.Invoke(msg);

        public void InvokeOnDestinationReachedByPlayer(NavMeshAgent agent) =>
            OnDestinationReachedByPlayer?.Invoke(agent);

        public void InvokeOnDestinationReachedByOppositeAgent() => OnDestinationReachedByOppositeAgent?.Invoke();
    }
}