using Game.Data;
using Game.Events;
using Game.Utils;
using Game.Validation;
using UnityEngine;
using VContainer;

namespace Game.GameObjects
{
    public class PointAndClickService
    {
        private PointAndClickData _data;
        private EventManager _eventManager;
        private INavMeshAgentService _agentService;

        [Inject]
        public PointAndClickService(PointAndClickData data, EventManager eventManager,
            INavMeshAgentService agentService)
        {
            _data = data;
            _eventManager = eventManager;
            _agentService = agentService;
        }

        public void HandleClick(Vector2 mousePosition)
        {
            Debug.Log("HandleClick");
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
            if (Physics.Raycast(ray, out RaycastHit _hitInfo, _data.MaxRaycastDistance, _data.ApproachableLayerMask))
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

        private void SetPlayerAgentDestination(Vector3 destination, Collider collider = null)
        {
            if (collider != null)
            {
                //Set agent destination
                var destVectors = collider.GetComponent<IApproachable>().GetPossibleDestinationPoints();
                var finalDestVector =
                    VectorsUtility.FindNearestVector3(_agentService.GetDestination(), destVectors);
                _agentService.SetDestination(finalDestVector);

                if (collider.TryGetComponent(out IInteractable interactable))
                    _data.CachedInteractable = interactable;
                else
                {
                    _data.CachedInteractable = null;
                }
            }
            else
            {
                _agentService.SetDestination(destination);
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

            if (!_agentService.IsAnyActiveAgent())
            {
                _eventManager.InvokeOnHandleClickValidationFailed(ValidationMessages.POINT_AND_CLICK_AGENT_NOT_EXIST);
                return false;
            }

            return true;
        }
    }
}