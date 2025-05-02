using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    /// <summary>
    /// Use physics raycast hit from mouse click to set agent destination
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class ClickToMove : MonoBehaviour
    {
        [SerializeField] private LayerMask m_NavMeshLayerMask;
        [SerializeField] private float m_maxRaycastDistance;
        private NavMeshAgent m_Agent;
        private RaycastHit m_HitInfo = new RaycastHit();

        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo, m_maxRaycastDistance, m_NavMeshLayerMask))
                    m_Agent.destination = m_HitInfo.point;
            }
        }
    }
}