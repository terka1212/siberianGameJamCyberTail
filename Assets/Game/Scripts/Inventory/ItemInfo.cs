using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "NewItemInfo", menuName = "Utilities/Inventory/Item")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] public int id;
        [SerializeField] public string Nname;
        [SerializeField] public Sprite icon;
    }
}