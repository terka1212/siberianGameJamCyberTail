using System.Collections;
using Game.Inventory;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToDo : MonoBehaviour
    {
        [SerializeField] private LayerMask m_NavMeshLayerMask;
        [SerializeField] private LayerMask m_UILayerMask;
        [SerializeField] private LayerMask m_PickableLayerMask;
        [SerializeField] private float m_maxRaycastDistance;
        [SerializeField] private float m_maxUIRaycastDistance;
        private NavMeshAgent m_Agent;
        private RaycastHit m_HitInfo = new RaycastHit();
        
        private bool CoroutineIsRunningPUII = false;
        private PickableItemView cachedPickableItem = null;
        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !MouseState.isPickedUp && !MouseState.isInDialog)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                //If Ray hits ui element
                if (RaycastUtilities.PointerIsOverUI(Input.mousePosition))
                {
                    Debug.Log("Hit UI");
                    return;
                }

                //if Ray hits PickableObject
                if (Physics.Raycast(ray, out m_HitInfo, m_maxRaycastDistance,m_PickableLayerMask))
                {
                    HandlePickableObjectHit(m_HitInfo.collider.GetComponent<PickableItemView>());
                    return;
                }
                
                //If Ray hits navmesh
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo, m_maxRaycastDistance, m_NavMeshLayerMask))
                {
                    cachedPickableItem = null;
                    m_Agent.destination = m_HitInfo.point;
                    Debug.Log("Hit navmesh");
                }
            }
        }

        private void HandlePickableObjectHit(PickableItemView item)
        {
            if (item == null)
            {
                Debug.Log("Item is null");
                return;
            }
            if (CoroutineIsRunningPUII)
            {
                Debug.Log("Stop Coroutine");
                return;
            }
            m_Agent.destination = item.GetFrontPoint();
            cachedPickableItem = item;
            StartCoroutine(PickUpInInventory(item));
            Debug.Log("Hit Pickable");
        }

        private IEnumerator PickUpInInventory(PickableItemView item)
        {
            CoroutineIsRunningPUII = true;
            while (!NavMeshAgentCheckForCompletion())
            {
                if (cachedPickableItem is null)
                {
                    CoroutineIsRunningPUII = false;
                    yield break;
                }
                yield return null;
            }

            Debug.Log("Picked up in inventory");
            Storage.AddItemById(item.itemID);
            Destroy(item.gameObject);
            CoroutineIsRunningPUII = false;
        }

        private bool NavMeshAgentCheckForCompletion()
        {
            // Check if we've reached the destination
            if (!m_Agent.pathPending)
            {
                if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                {
                    if (!m_Agent.hasPath || m_Agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}