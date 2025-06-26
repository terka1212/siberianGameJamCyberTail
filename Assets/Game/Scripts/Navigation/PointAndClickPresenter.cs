using System;
using Game.Infrastructure.ScopedLifecycle;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.GameObjects
{
    public class PointAndClickPresenter : IScopedStartable, IDisposable
    {
        private PointAndClickService _pointAndClickService;
            
        private InputAction _clickAction;
        private InputAction _mousePositionAction;
        private ScopedLifecycleManager _scopedLifecycleManager;

        [Inject]
        public PointAndClickPresenter(PointAndClickService pointAndClickService, ScopedLifecycleManager scopedLifecycleManager)
        {
            _pointAndClickService = pointAndClickService;
            _scopedLifecycleManager = scopedLifecycleManager;
            _scopedLifecycleManager.Register(this);
        }
        
        public void ScopedStart()
        {
            _clickAction = InputSystem.actions.FindAction("Click");
            _mousePositionAction = InputSystem.actions.FindAction("CursorPosition");
            
            _clickAction.performed += ClickHandle;
        }

        private void ClickHandle(InputAction.CallbackContext context)
        {
            _pointAndClickService.HandleClick(_mousePositionAction.ReadValue<Vector2>());
        }
            

        public void Dispose()
        {
            _clickAction.performed -= ClickHandle;
            
            _scopedLifecycleManager.Unregister(this);
        }

        
    }
}