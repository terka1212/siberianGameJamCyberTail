using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image slotImage;
        public int SlotNumber { get; set; }

        public void SetImage(Sprite sprite)
        {
            slotImage.sprite = sprite;
        }
        
        
    }
}