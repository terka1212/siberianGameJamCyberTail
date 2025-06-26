using System;
using Game.Events;
using Game.GameObjects;
using Game.Infrastructure.ScopedLifecycle;
using UnityEngine;
using VContainer.Unity;

namespace Game.Data
{
    public class PointAndClickData : IStartable, IDisposable, IBlockable
    {
        public bool isBlocked { get; private set; }
        
        public IInteractable CachedInteractable { get; set; }
        
        public LayerMask NavMeshLayerMask { get; private set; }
        public LayerMask UILayerMask { get; private set; }
        public LayerMask ApproachableLayerMask { get; private set; }
        
        public float MaxRaycastDistance { get; private set; }
        
        private EventManager _eventManager;

        public PointAndClickData(EventManager eventManager, float maxRaycastDistance = 15f, bool isBlocked = true)
        {
            _eventManager = eventManager;
            NavMeshLayerMask = LayerMask.GetMask("Navmesh");
            UILayerMask = LayerMask.GetMask("UI");
            ApproachableLayerMask = LayerMask.GetMask("Approachable");
            MaxRaycastDistance = maxRaycastDistance;
            this.isBlocked = isBlocked;
        }

        public void Start()
        {
            _eventManager.OnStartSceneTransitionEvent += OnTransitionStarted;
            _eventManager.OnEndSceneTransitionEvent += OnTransitionEnded;
        }

        public void Block()
        {
            isBlocked = true;
            _eventManager.InvokeOnPointAndClickBlocked();
        }

        public void Unblock()
        {
            isBlocked = false;
            _eventManager.InvokeOnPointAndClickUnblocked();
        }

        private void OnTransitionStarted(SceneName sceneName, SceneName nextSceneName)
        {
            Block();
        }
        
        private void OnTransitionEnded(SceneName sceneName, SceneName nextSceneName)
        {
            Unblock();
        }

        public void Dispose()
        {
            _eventManager.OnStartSceneTransitionEvent -= OnTransitionStarted;
            _eventManager.OnEndSceneTransitionEvent -= OnTransitionEnded;
        }
    }
}