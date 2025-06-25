using UnityEngine;

namespace Game.GameObjects
{
    public interface INavMeshAgentService
    {
        public void SetDestination(Vector3 destination);
        public Vector3 GetDestination();
        public bool IsAnyActiveAgent();
    }
}