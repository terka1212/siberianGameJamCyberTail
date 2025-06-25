using Game.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Animation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentAnimationSynchronizer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float velocityToDetect;

        private float _prevX;
        private NavMeshAgent _agent;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var velocity = _agent.velocity.magnitude;
            
            //Handle Animator isMoving bool
            _animator.SetBool(Constants.NAVMESH_ANIM_BOOL_ISMOVING, velocity > velocityToDetect);

            //Change direction if prev x is different
            var x = transform.position.x;
            
            if (!Mathf.Approximately(_prevX, x))
            {
                ChangeDirection(_prevX, x);
            }

            _prevX = x;
        }
        
        private void ChangeDirection(float prevVal, float val)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                val > prevVal ? 180 : 0, //y rotation
                transform.rotation.eulerAngles.z);
        }
    }
}