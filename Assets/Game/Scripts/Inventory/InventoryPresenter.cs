using System.Collections.Generic;
using Game.Data;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class InventoryPresenter
    {
        private InventoryService _service;

        [Inject]
        public InventoryPresenter(InventoryService service)
        {
            _service = service;
        }

        public void PutItemInInventory(int itemId)
        {
            _service.PutItemInInventory(itemId);
        }

        public void SwapSlots(int fromId, int toId)
        {
            _service.SwapSlots(fromId, toId);
        }

        public bool TakeItemFromInventory(int itemId)
        {
            return _service.TakeItemFromInventory(itemId);
        }

        public bool HasItemInInventory(int itemId)
        {
            return _service.HasItemInInventory(itemId);
        }

        public Sprite GetItemSprite(int itemId)
        {
            return _service.GetItemSprite(itemId);
        }

        public Sprite GetEmptySlotSprite()
        {
            return _service.GetEmptySlotSprite();
        }

        public List<InventorySlotData> GetPage(int page)
        {
            return _service.GetPage(page);
        }
        
        public List<InventorySlotData> GetCurrentPage()
        {
            return _service.GetCurrentPage();
        }
    }
}