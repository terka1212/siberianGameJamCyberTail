using System;
using Game.Events;
using VContainer;
using VContainer.Unity;

namespace Game.Dialogues.NPC
{
    public class InteractableHandlingPresenter : IStartable, IDisposable
    {
        private EventManager _eventManager;
        private InteractableHandlingService _interactableHandlingService;

        [Inject]
        public InteractableHandlingPresenter(InteractableHandlingService interactableHandlingService,
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