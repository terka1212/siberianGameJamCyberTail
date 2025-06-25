using UnityEngine;

namespace Game.GameObjects
{
    public class NullNavMeshAgentService : INavMeshAgentService
    {
        public void SetDestination(Vector3 destination)
        {
        }

        public Vector3 GetDestination()
        {
            return Vector3.zero;
        }

        public bool IsAnyActiveAgent()
        {
            return false;
        }
    }
}