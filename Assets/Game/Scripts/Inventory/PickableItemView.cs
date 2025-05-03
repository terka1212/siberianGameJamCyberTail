using System;
using UnityEngine;

namespace Game.Inventory
{
    public class PickableItemView : MonoBehaviour
    {
        [SerializeField] public int itemID;
        
        private void Awake()
        {
            if (AllItems.isItemInInventory(itemID))
            {
                Destroy(gameObject);
            }
        }

        public Vector3 GetFrontPoint(LayerMask mask)
        {
            //11 - navigation layer
            if (Physics.Raycast(transform.position + Vector3.back, Vector3.up, out RaycastHit hit, 30f, mask))
            {
                Debug.Log("Hit point: " + hit.point);
                return hit.point;
            }
            
            Debug.Log("No hit point");

            return transform.position + Vector3.back;
        }
    }
}