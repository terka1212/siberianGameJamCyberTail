using System.Collections.Generic;
using Game.Events;
using UnityEngine;

namespace Game.Inventory
{
    public static class Storage
    {
        private static Dictionary<int, Item> items = new Dictionary<int, Item>();

        private const int MAX_SLOTS = 5;

        public static void Init()
        {
            for (int i = 0; i < MAX_SLOTS; i++)
            {
                items[i] = null;
            }
        }

        public static bool isSlotNotEmpty(int slot)
        {
            return items.ContainsKey(slot) && items[slot] != null;
        }
        
        public static Item GetItemByPosition(int position)
        {
            return items[position];
        }

        public static bool AddItemById(int id)
        {
            var item = AllItems.GetItemById(id);
            var i = 0;
            //find empty slot
            for (; i < MAX_SLOTS; i++)
            {
                if (items[i] is null)
                {
                    break;
                }
            }
            
            //if no empty slots
            if (i == MAX_SLOTS-1 && items[MAX_SLOTS-1] is not null)
            {
                Debug.Log("No more item slots available");
                return false;
            }

            //if has empty slots
            items[i] = item;
            item.InInventory = true;
            EventManager.InvokeInventoryChangeEvent();
            return true;
        }

        public static void RemoveItem(int position)
        {
            var item = items[position];
            if (item is not null)
            {
                item.InInventory = false;
            }
            items.Remove(position);
            EventManager.InvokeInventoryChangeEvent();
        }
    }
}