using System.Collections;
using Game.Dialogues.NPC;
using Game.Events;
using Game.Inventory;
using Game.SceneManagement;
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
        [SerializeField] private LayerMask navMeshLayerMask;
        [SerializeField] private LayerMask UILayerMask;
        [SerializeField] private LayerMask pickableLayerMask;
        [SerializeField] private LayerMask NPCLayerMask;
        [SerializeField] private float maxRaycastDistance;
        [SerializeField] private float velocityToDetect;
        [SerializeField] private Animator _animator;
        private NavMeshAgent _agent;
        private RaycastHit _hitInfo = new RaycastHit();

        private Coroutine _cachedPUII;
        private Coroutine _cachedTTN;

        private Direction direction = Direction.Left;
        private float previousX;
        
        private bool _coroutineIsRunningPUII = false;
        private bool _coroutineIsRunningTTN = false;
        private PickableItemView _cachedPickableItem = null;
        private NPC _cachedNPC = null;
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            EventManager.StartSceneLoading += HandleStartSceneLoading;
        }

        void Update()
        {
            //Handle movement, PickableObject, Npc
            if (Input.GetMouseButtonDown(0) && !MouseState.isPickedUp && !MouseState.isInDialog && !SceneLoader.isSceneLoading)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                //If Ray hits ui element
                if (RaycastUtilities.PointerIsOverUI(Input.mousePosition))
                {
                    Debug.Log("Hit UI");
                    return;
                }

                //if Ray hits PickableObject
                if (Physics.Raycast(ray, out _hitInfo, maxRaycastDistance,pickableLayerMask))
                {
                    HandlePickableObjectHit(_hitInfo.collider.GetComponent<PickableItemView>());
                    _cachedNPC = null;
                    return;
                }
                
                //If Ray hits NPCObject
                if(Physics.Raycast(ray, out _hitInfo, maxRaycastDistance,NPCLayerMask))
                {
                    HandleNPCObjectHit(_hitInfo.collider.GetComponent<NPC>());
                    _cachedPickableItem = null;
                    return;
                }
                
                //If Ray hits navmesh
                if (Physics.Raycast(ray.origin, ray.direction, out _hitInfo, maxRaycastDistance, navMeshLayerMask))
                {
                    _cachedPickableItem = null;
                    _cachedNPC = null;
                    _agent.destination = _hitInfo.point;
                    Debug.Log("Hit navmesh");
                }
            }
            
            //Handle animations
            var velocity = _agent.velocity.magnitude;
            if (velocity > velocityToDetect)
            {
                _animator.SetBool("IsMoving", true);
            }
            else
            {
                _animator.SetBool("IsMoving", false);
            }
            
            //Change direction if prev x is different
            var x = transform.position.x;
            if (!Mathf.Approximately(previousX, x))
            {
                ChangeDirection(previousX, x);
            }

            previousX = x;
            
            //Handle direction
            switch (direction)
            {
                case Direction.Left:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
                    break;
                case Direction.Right:
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
                    break;
            }
        }

        private void ChangeDirection(float prevVal, float val)
        {
            if (val > prevVal)
            {
                direction = Direction.Right;
            }
            else
            {
                direction = Direction.Left;
            }
        }

        private void HandleStartSceneLoading()
        {
            _cachedNPC = null;
            _cachedPickableItem = null;
        }

        private void HandlePickableObjectHit(PickableItemView item)
        {
            if (item == null)
            {
                Debug.Log("PickableItem is null");
                return;
            }
            if (_coroutineIsRunningPUII)
            {
                Debug.Log("Stop PUII Coroutine");
                StopCoroutine(_cachedPUII);
                return;
            }
            _agent.destination = item.GetFrontPoint(navMeshLayerMask);
            _cachedPickableItem = item;
            _cachedPUII = StartCoroutine(PickUpInInventory(item));
            Debug.Log("Hit Pickable");
        }
        
        private void HandleNPCObjectHit(NPC npc)
        {
            if (npc == null)
            {
                Debug.Log("NPC is null");
                return;
            }
            if (_coroutineIsRunningTTN)
            {
                Debug.Log("Stop TTN Coroutine");
                StopCoroutine(_cachedTTN);
                return;
            }
            _agent.destination = npc.GetFrontPoint(navMeshLayerMask);
            _cachedNPC = npc;
            _cachedTTN = StartCoroutine(TalkToNPC(npc));
            Debug.Log("Hit NPC");
        }

        private IEnumerator TalkToNPC(NPC npc)
        {
            _coroutineIsRunningTTN = true;
            while (!NavMeshAgentCheckForCompletion())
            {
                if (_cachedNPC is null)
                {
                    _coroutineIsRunningTTN = false;
                    yield break;
                }
                yield return null;
            }

            Debug.Log("Talk to NPC");
            npc.Interact();
            _coroutineIsRunningTTN = false;
        }

        private IEnumerator PickUpInInventory(PickableItemView item)
        {
            _coroutineIsRunningPUII = true;
            while (!NavMeshAgentCheckForCompletion())
            {
                if (_cachedPickableItem is null)
                {
                    _coroutineIsRunningPUII = false;
                    yield break;
                }
                yield return null;
            }

            Debug.Log("Picked up in inventory");
            Storage.AddItemById(item.itemID);
            Destroy(item.gameObject);
            _coroutineIsRunningPUII = false;
        }

        private bool NavMeshAgentCheckForCompletion()
        {
            // Check if we've reached the destination
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnDestroy()
        {
            EventManager.StartSceneLoading-= HandleStartSceneLoading;
        }
    }

    public enum Direction
    {
        Right, 
        Left
    }
}