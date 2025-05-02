using UnityEngine;

namespace Game.Inventory
{
    public class PickableItemView : MonoBehaviour
    {
        [SerializeField] public int itemID;
        
        [SerializeField] public bool isBlocked = false;

        public Vector2 GetFrontPoint()
        {
            //11 - navigation layer
            if(Physics.Raycast(transform.position + Vector3.back, Vector3.down, out RaycastHit hit, 10f, 11))
                return hit.point;
            return transform.position;
        }
    }
}