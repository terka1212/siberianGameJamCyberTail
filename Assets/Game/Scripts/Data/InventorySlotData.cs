namespace Game.Data
{
    public class InventorySlotData
    {
        public int Slot { get; private set; } = 0;

        public bool IsEmpty { get; private set; } = true;

        public void TakeFromSlot()
        {
            Slot = -1;
            IsEmpty = true;
        }

        public void PutIntoSlot(int itemId)
        {
            Slot = itemId;
            IsEmpty = false;
        }
    }
}