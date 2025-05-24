using Game.Dialogues.NPC;
using UnityEngine;

namespace Game.Data
{
    public class PointAndClickData
    {
        public bool isBlocked { get; set; }
        
        public IInteractable CachedInteractable { get; set; }
        
        public LayerMask NavMeshLayerMask { get; private set; }
        public LayerMask UILayerMask { get; private set; }
        public LayerMask InteractableLayerMask { get; private set; }
        
        public float MaxRaycastDistance { get; private set; }

        public PointAndClickData(float maxRaycastDistance = 15f, bool isBlocked = true)
        {
            NavMeshLayerMask = LayerMask.GetMask("Navmesh");
            UILayerMask = LayerMask.GetMask("UI");
            InteractableLayerMask = LayerMask.GetMask("Interactable");
            MaxRaycastDistance = maxRaycastDistance;
            this.isBlocked = isBlocked;
        }
    }
}