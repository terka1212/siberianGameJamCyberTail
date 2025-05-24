using Game.Data;
using Game.Events;
using Game.Navigation;
using UnityEngine.AI;
using VContainer;

namespace Game.Dialogues.NPC
{
    public class InteractableHandlingService
    {
        private PlayerNavMeshAgentService _playerNavMeshAgentService;
        private PointAndClickData _pointAndClickData;
        private EventManager _eventManager;

        [Inject]
        public InteractableHandlingService(PlayerNavMeshAgentService playerNavMeshAgentService,
            PointAndClickData pointAndClickData, EventManager eventManager)
        {
            _playerNavMeshAgentService = playerNavMeshAgentService;
            _pointAndClickData = pointAndClickData;
            _eventManager = eventManager;
        }

        public void HandleInteraction(NavMeshAgent agent)
        {
            if (_playerNavMeshAgentService.IsSameAgent(agent))
            {
                _pointAndClickData.CachedInteractable?.Interact();
                _pointAndClickData.CachedInteractable = null;
            }
            else
            {
                _eventManager.InvokeOnDestinationReachedByOppositeAgent();
            }
        }
    }
}