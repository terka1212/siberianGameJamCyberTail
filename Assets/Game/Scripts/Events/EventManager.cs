using System;

namespace Game.Events
{
    public class EventManager
    {
        public static event Action InventoryChange;
        
        public static void InvokeInventoryChangeEvent() => InventoryChange?.Invoke();
    }
}