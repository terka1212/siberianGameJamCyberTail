using System;
using System.Collections.Generic;
using Game.Data;
using Game.Events;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

namespace Game.Inventory
{
    public class InventoryService
    {
        private InventoryData _inventoryData;
        private InventorySpriteData _inventorySpriteData;
        private EventManager _eventManager;

        [Inject]
        public InventoryService(InventoryData inventoryData, InventorySpriteData inventorySpriteData,
            EventManager eventManager)
        {
            _inventoryData = inventoryData;
            _inventorySpriteData = inventorySpriteData;
            _eventManager = eventManager;
        }

        public void SwapSlots(int fromId, int toId)
        {
            _inventoryData.Slots.TrySwap(fromId, toId, out var exception);
            if (exception != null)
            {
                _eventManager.InvokeOnInventorySlotSwapFailed(exception.Message);
            }
        }

        public void PutItemInInventory(int itemId)
        {
            _inventoryData.Put(itemId);
        }

        public bool TakeItemFromInventory(int itemId)
        {
            return _inventoryData.Has(itemId) && _inventoryData.Take(itemId);
        }

        public bool HasItemInInventory(int itemId)
        {
            return _inventoryData.Has(itemId);
        }

        public Sprite GetItemSprite(int itemId)
        {
            return _inventorySpriteData.Sprites.TryGetValue(itemId, out Sprite sprite)
                ? sprite
                : _inventorySpriteData.DefaultSprite;
        }

        public Sprite GetEmptySlotSprite()
        {
            return _inventorySpriteData.EmptySlotSprite == null
                ? _inventorySpriteData.DefaultSprite
                : _inventorySpriteData.EmptySlotSprite;
        }

        public List<InventorySlotData> GetPage(int page)
        {
            page %= _inventoryData.MaxPages;
            _inventoryData.CurrPage = page;
            return _inventoryData.Slots.GetRange(page * _inventoryData.PageCapacity,
                _inventoryData.PageCapacity);
        }

        public List<InventorySlotData> GetCurrentPage()
        {
            return GetPage(_inventoryData.CurrPage);
        }
    }
}