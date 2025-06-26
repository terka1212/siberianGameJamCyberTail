using System.Collections.Generic;
using UnityEngine;

namespace Game.Infrastructure.ScopedLifecycle
{
    public class ScopedLifecycleManager : MonoBehaviour
    {
        private readonly List<IScopedStartable> _scopedStartables = new List<IScopedStartable>();
        private readonly List<IScopedTickable> _scopedTickables = new List<IScopedTickable>();

        private bool _isStarted;

        public void Register(object instance)
        {
            if (instance is IScopedStartable startable)
            {
                _scopedStartables.Add(startable);
                if (_isStarted) startable.ScopedStart();
            }

            if (instance is IScopedTickable tickable)
            {
                _scopedTickables.Add(tickable);
            }
        }

        public void Unregister(object instance)
        {
            if (instance is IScopedStartable startable) _scopedStartables.Remove(startable);
            if (instance is IScopedTickable tickable) _scopedTickables.Remove(tickable);
        }

        private void Start()
        {
            _isStarted = true;
            foreach (var startable in _scopedStartables.ToArray())
            {
                startable.ScopedStart();
            }
        }

        private void Update()
        {
            foreach (var tickable in _scopedTickables.ToArray())
            {
                tickable.ScopedTick();
            }
        }
    }
}