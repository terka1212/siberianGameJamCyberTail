using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Game.Navigation
{
    public class PointAndClickPresenter : IStartable, IDisposable
    {
        private PointAndClickService _pointAndClickService;
            
        private InputAction _clickAction;
        private InputAction _mousePositionAction;

        [Inject]
        public PointAndClickPresenter(PointAndClickService pointAndClickService)
        {
            _pointAndClickService = pointAndClickService;
        }
        
        public void Start()
        {
            _clickAction = InputSystem.actions.FindAction("Click");
            _mousePositionAction = InputSystem.actions.FindAction("MousePosition");
            
            _clickAction.performed += ClickHandle;
        }

        private void ClickHandle(InputAction.CallbackContext context)
        {
            _pointAndClickService.HandleClick(_mousePositionAction.ReadValue<Vector2>());
        }
            

        public void Dispose()
        {
            _clickAction.performed -= ClickHandle;
            
            _clickAction?.Dispose();
            _mousePositionAction?.Dispose();
        }

        
    }
}