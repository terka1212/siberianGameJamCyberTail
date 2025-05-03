using Game.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Inventory
{
    [RequireComponent(typeof(Image))]
    public class ItemSlotView : MonoBehaviour
    {
        [SerializeField] private int slotNumber;
        
        private Item _item;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            if(Storage.isSlotNotEmpty(slotNumber))
                OnInventoryChange();
        }
        private void Start()
        {
            EventManager.InventoryChange += OnInventoryChange;
        }

        private void OnInventoryChange()
        {
            var item = Storage.GetItemByPosition(slotNumber);
            if (item is null || !item.InInventory) return;
            _item = item;
            _image.sprite = _item.ItemInfo.icon;
        }

        private void OnDestroy()
        {
            EventManager.InventoryChange -= OnInventoryChange;
        }
        
        
    }
}