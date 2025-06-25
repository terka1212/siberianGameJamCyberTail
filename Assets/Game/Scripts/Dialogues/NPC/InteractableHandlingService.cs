using Game.Data;
using Game.GameObjects;
using UnityEngine.AI;
using VContainer;

namespace Game.Dialogues.NPC
{
    public class InteractableHandlingService
    {
        private INavMeshAgentService _navMeshAgentService;
        private PointAndClickData _pointAndClickData;

        [Inject]
        public InteractableHandlingService(INavMeshAgentService navMeshAgentService,
            PointAndClickData pointAndClickData)
        {
            _navMeshAgentService = navMeshAgentService;
            _pointAndClickData = pointAndClickData;
        }

        public void HandleInteraction(NavMeshAgent agent)
        {
                _pointAndClickData.CachedInteractable?.Interact();
                _pointAndClickData.CachedInteractable = null;
        }
    }
}