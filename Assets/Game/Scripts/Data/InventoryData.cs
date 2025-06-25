using System.Collections.Generic;
using Game.Events;
using UnityEngine;

namespace Game.Data
{
    public class InventoryData
    {
        public List<InventorySlotData> Slots;
        
        public int CurrPage
        {
            get => _currPage;
            set
            {
                _currPage = value;
                _eventManager.InvokeOnInventoryPageChanged();
            }
        }

        public readonly int MaxPages;
        public readonly int PageCapacity;
        
        private readonly EventManager _eventManager;
        private int _currPage;

        public InventoryData(int pageCapacity, int maxPages, EventManager eventManager)
        {
            _eventManager = eventManager;
            Slots = new List<InventorySlotData>(maxPages * pageCapacity);
            PageCapacity = pageCapacity;
            MaxPages = maxPages;
            _currPage = 0;
            Init();
        }

        private void Init()
        {
            for (var i = 0; i < MaxPages*PageCapacity; i++)
            {
                Slots.Add(new InventorySlotData());
            }
        }

        public bool Has(int itemId)
        {
            var result = Slots.Find(x => x.Slot == itemId && !x.IsEmpty);
            return result is not null;
        }

        public bool Put(int itemId)
        {
            if (Has(itemId)) return false;
            var emptySlot = HasEmptySlots();
            if (!emptySlot.vacant) return false;

            var pageNumber = Mathf.CeilToInt((float)emptySlot.slotIndex / PageCapacity);
            CurrPage = pageNumber;
            Slots[emptySlot.slotIndex].PutIntoSlot(itemId);
            return true;
        }

        public bool Take(int itemId)
        {
            if (!Has(itemId)) return false;
            
            var slotWithItem = Slots.Find(x => x.Slot == itemId && !x.IsEmpty);
            slotWithItem.TakeFromSlot();
            return true;
        }

        private (bool vacant, int slotIndex) HasEmptySlots()
        {
            bool vacant = false;
            int slotIndex = -1;
            for (var i = 0; i < Slots.Count; i++)
            {
                if (!Slots[i].IsEmpty) continue;
                vacant = true;
                slotIndex = i;
                break;
            }

            return (vacant, slotIndex);
        }
    }
}