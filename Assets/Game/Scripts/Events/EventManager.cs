using System;
using Game.Data;
using UnityEngine.AI;

namespace Game.Events
{
    public class EventManager
    {
        //TODO удалить 2 строки ниже
        public static event Action InventoryChange;

        public static void InvokeInventoryChangeEvent() => InventoryChange?.Invoke();

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

        //Inventory Events
        public event Action<string> OnInventorySlotSwapFailed;
        public event Action OnInventoryPageChanged;

        //Scene Events
        public event Action<SceneName, SceneName> OnStartSceneTransitionEvent;
        public event Action<SceneName, SceneName> OnEndSceneTransitionEvent;

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

        //Inventory Events Handling
        public void InvokeOnInventorySlotSwapFailed(string msg) => OnInventorySlotSwapFailed?.Invoke(msg);
        public void InvokeOnInventoryPageChanged() => OnInventoryPageChanged?.Invoke();

        //Scene Events Handling
        public void InvokeOnStartSceneTransitionEvent(SceneName sceneLoadFrom, SceneName sceneLoadTo) =>
            OnStartSceneTransitionEvent?.Invoke(sceneLoadFrom, sceneLoadTo);

        public void InvokeOnEndSceneTransitionEvent(SceneName sceneLoadFrom, SceneName sceneLoadTo) =>
            OnEndSceneTransitionEvent?.Invoke(sceneLoadFrom, sceneLoadTo);
    }
}