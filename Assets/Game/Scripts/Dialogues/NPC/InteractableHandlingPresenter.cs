using System;
using Game.Events;
using UnityEngine;
using VContainer;

namespace Game.Dialogues.NPC
{
    public class InteractableHandlingPresenter : MonoBehaviour, IDisposable
    {
        private EventManager _eventManager;
        private InteractableHandlingService _interactableHandlingService;

        [Inject]
        public void Construct(InteractableHandlingService interactableHandlingService,
            EventManager eventManager)
        {
            _interactableHandlingService = interactableHandlingService;
            _eventManager = eventManager;
        }

        public void Start()
        {
            _eventManager.OnDestinationReachedByPlayer += _interactableHandlingService.HandleInteraction;
        }

        public void Dispose()
        {
            _eventManager.OnDestinationReachedByPlayer -= _interactableHandlingService.HandleInteraction;
        }
    }
}