using Game.Events;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;

namespace Game.Navigation
{
    public class PlayerNavMeshAgentService : ITickable
    {
        private NavMeshAgent _agent;
        private EventManager _eventManager;
        
        private bool _verifyCompletion = false;

        [Inject]
        public PlayerNavMeshAgentService(EventManager eventManager,NavMeshAgent agent = null)
        {
            _eventManager = eventManager;
            _agent = agent;
        }

        public void Tick()
        {
            // Check if we've reached the destination
            if (!_verifyCompletion) return;
            
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        DestinationReached();
                    }
                }
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _agent?.SetDestination(destination);
            _verifyCompletion = true;
        }

        public Vector3 GetDestination()
        {
            return _agent == null ? Vector3.positiveInfinity : _agent.destination;
        }

        public bool IsAnyActiveAgent()
        {
            return _agent != null;
        }

        public bool IsSameAgent(NavMeshAgent other)
        {
            return other.Equals(_agent);
        }

        private void DestinationReached()
        {
            _verifyCompletion = false;
            _eventManager.InvokeOnDestinationReachedByPlayer(_agent);
        }
    }
}