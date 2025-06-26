using System;
using Game.Events;
using Game.Infrastructure.ScopedLifecycle;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Game.GameObjects
{
    public class NavMeshAgentService : IScopedTickable, INavMeshAgentService, IDisposable
    {
        private NavMeshAgent _agent;
        private EventManager _eventManager;
        private ScopedLifecycleManager _scopedLifecycleManager;
        
        private bool _verifyCompletion = false;

        [Inject]
        public void Construct(EventManager eventManager, ScopedLifecycleManager scopedLifecycleManager,NavMeshAgent agent = null)
        {
            _eventManager = eventManager;
            _scopedLifecycleManager = scopedLifecycleManager;
            _scopedLifecycleManager.Register(this);
            _agent = agent;
        }

        public void ScopedTick()
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

        private void DestinationReached()
        {
            _verifyCompletion = false;
            Debug.Log("DestinationReached");
            _eventManager.InvokeOnDestinationReachedByPlayer(_agent);
        }

        public void Dispose()
        {
            _scopedLifecycleManager.Unregister(this);
        }
    }
}