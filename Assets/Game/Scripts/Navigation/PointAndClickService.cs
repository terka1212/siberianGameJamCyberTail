using Game.Data;
using Game.Dialogues.NPC;
using Game.Events;
using Game.Infrastructure;
using Game.Utils;
using Game.Validation;
using UnityEngine;
using VContainer;

namespace Game.Navigation
{
    public class PointAndClickService : IBlockable
    {
        private PointAndClickData _data;
        private EventManager _eventManager;
        private PlayerNavMeshAgentService _playerAgentService;

        [Inject]
        public PointAndClickService(PointAndClickData data, EventManager eventManager,
            PlayerNavMeshAgentService playerAgentService)
        {
            _data = data;
            _eventManager = eventManager;
            _playerAgentService = playerAgentService;
        }

        public void HandleClick(Vector2 mousePosition)
        {
            if (!ValidateClick()) return;
            var ray = Camera.main.ScreenPointToRay(mousePosition);

            //If Ray hits ui element
            if (RaycastUtilities.PointerIsOverUI(Input.mousePosition))
            {
#if (UNITY_EDITOR)
                Debug.Log("Hit UI");
#endif
                return;
            }

            //Ray hits IApproachable
            if (Physics.Raycast(ray, out RaycastHit _hitInfo, _data.MaxRaycastDistance, _data.InteractableLayerMask))
            {
                SetPlayerAgentDestination(_hitInfo.point, _hitInfo.collider);
#if (UNITY_EDITOR)
                Debug.Log("Hit IApproachable");
#endif
                return;
            }

            //If Ray hits navmesh
            if (Physics.Raycast(ray.origin, ray.direction, out _hitInfo, _data.MaxRaycastDistance,
                    _data.NavMeshLayerMask))
            {
                SetPlayerAgentDestination(_hitInfo.point);
#if (UNITY_EDITOR)
                Debug.Log("Hit navmesh");
#endif
            }
        }

        public bool IsBlocked()
        {
            return _data.isBlocked;
        }

        public void Block()
        {
            _data.isBlocked = true;
            _eventManager.InvokeOnPointAndClickBlocked();
        }

        public void Unblock()
        {
            _data.isBlocked = false;
            _eventManager.InvokeOnPointAndClickUnblocked();
        }

        private void SetPlayerAgentDestination(Vector3 destination, Collider collider = null)
        {
            if (collider != null)
            {
                //Set agent destination
                var destVectors = collider.GetComponent<IApproachable>().GetPossibleDestinationPoints();
                var finalDestVector =
                    VectorsUtility.FindNearestVector3(_playerAgentService.GetDestination(), destVectors);
                _playerAgentService.SetDestination(finalDestVector);

                if (collider.TryGetComponent(out IInteractable interactable))
                    _data.CachedInteractable = interactable;
            }
            else
            {
                _playerAgentService.SetDestination(destination);
                _data.CachedInteractable = null;
            }
        }

        private bool ValidateClick()
        {
            if (IsBlocked())
            {
                _eventManager.InvokeOnHandleClickValidationFailed(ValidationMessages.POINT_AND_CLICK_BLOCKED);
                return false;
            }

            if (!_playerAgentService.IsAnyActiveAgent())
            {
                _eventManager.InvokeOnHandleClickValidationFailed(ValidationMessages.POINT_AND_CLICK_AGENT_NOT_EXIST);
                return false;
            }

            return true;
        }
    }
}