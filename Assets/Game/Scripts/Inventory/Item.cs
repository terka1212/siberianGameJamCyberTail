using System;

namespace Game.Inventory
{
    [Serializable]
    public class Item
    {
        [NonSerialized]
        public bool InInventory;
        [NonSerialized]
        public bool PickedUpFromInventory;
        public ItemInfo ItemInfo;

        public void PickUp()
        {
            if (!InInventory) return;
            
            PickedUpFromInventory = true;
            MouseState.isPickedUp = true;
            MouseState.pickedUpItem = this;
        }

        public void Drop()
        {
            if (!InInventory) return;
            
            PickedUpFromInventory = false;
            MouseState.isPickedUp = false;
            MouseState.pickedUpItem = null;
        }
    }
}