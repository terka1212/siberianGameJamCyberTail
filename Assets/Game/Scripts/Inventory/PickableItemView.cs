using UnityEngine;

namespace Game.Inventory
{
    public class PickableItemView : MonoBehaviour
    {
        [SerializeField] public int itemID;
        
        [SerializeField] public bool isBlocked = false;

        public Vector2 GetFrontPoint()
        {
            if(Physics.Raycast(transform.position + Vector3.back, Vector3.down, out RaycastHit hit))
                return hit.point;
            return transform.position;
        }
    }
}