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
        
        //DialogueEvents
        public event Action OnDialogueStarted;
        public event Action OnEndDialogue;
        public event Action<long, int> OnNPCDialogueNull;
        public event Action<long> OnNPCNotExisted;
        
        //AudioEvents
        public event Action<string> OnEffectDontFound;
        public event Action OnSoundTypeIsntHandle;
        public event Action OnSoundSettingTypeIsntHandle;

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
        
        //Dialogue Events Handling
        public void InvokeOnDialogueStarted() => OnDialogueStarted?.Invoke();
        public void InvokeOnEndDialogue() => OnEndDialogue?.Invoke();
        public void InvokeOnNPCDialogueNull(long npcId, int progress) => OnNPCDialogueNull?.Invoke(npcId, progress);
        public void InvokeOnNPCNotExisted(long npcId) => OnNPCNotExisted?.Invoke(npcId);
        
        //Audio Events Handling
        public void InvokeOnEffectDontFound(string msg) => OnEffectDontFound?.Invoke(msg);
        public void InvokeOnSoundTypeIsntHandle() => OnSoundTypeIsntHandle?.Invoke();
        public void InvokeOnSoundSettingTypeIsntHandle() => OnSoundSettingTypeIsntHandle?.Invoke();
    }
}