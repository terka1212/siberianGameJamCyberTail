using Game.Data;
using UnityEngine.AI;
using VContainer;

namespace Game.Dialogues.NPC
{
    public class InteractableHandlingService
    {
        private PointAndClickData _pointAndClickData;

        [Inject]
        public InteractableHandlingService(PointAndClickData pointAndClickData)
        {
            _pointAndClickData = pointAndClickData;
        }

        public void HandleInteraction(NavMeshAgent agent)
        {
            _pointAndClickData.CachedInteractable?.Interact();
            _pointAndClickData.CachedInteractable = null;
        }
    }
}