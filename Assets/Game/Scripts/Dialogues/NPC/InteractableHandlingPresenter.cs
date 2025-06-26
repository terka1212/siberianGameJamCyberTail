using System;
using Game.Events;
using Game.Infrastructure.ScopedLifecycle;
using VContainer;

namespace Game.Dialogues.NPC
{
    public class InteractableHandlingPresenter : IScopedStartable, IDisposable
    {
        private EventManager _eventManager;
        private InteractableHandlingService _interactableHandlingService;
        private ScopedLifecycleManager _scopedLifecycleManager;

        [Inject]
        public void Construct(InteractableHandlingService interactableHandlingService,
            EventManager eventManager, ScopedLifecycleManager scopedLifecycleManager)
        {
            _interactableHandlingService = interactableHandlingService;
            _eventManager = eventManager;
            _scopedLifecycleManager = scopedLifecycleManager;
            _scopedLifecycleManager.Register(this);
        }

        public void ScopedStart()
        {
            _eventManager.OnDestinationReachedByPlayer += _interactableHandlingService.HandleInteraction;
        }

        public void Dispose()
        {
            _eventManager.OnDestinationReachedByPlayer -= _interactableHandlingService.HandleInteraction;
            _scopedLifecycleManager.Unregister(this);
        }
    }
}