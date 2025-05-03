using System;

namespace Game.Events
{
    public class EventManager
    {
        public static event Action InventoryChange;

        public static event Action StartSceneLoading;
        
        public static void InvokeInventoryChangeEvent() => InventoryChange?.Invoke();
        
        public static void InvokeStartSceneLoading() => StartSceneLoading?.Invoke();
    }
}