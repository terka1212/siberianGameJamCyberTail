using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Data
{
    
    [CreateAssetMenu(fileName = "NewInventorySprites", menuName = "Utilities/Inventory/InventorySprites")]
    public class InventorySpriteData : ScriptableObject
    {
        [SerializeField] public SerializedDictionary<int, Sprite> Sprites;
        [SerializeField] public Sprite DefaultSprite;
        [SerializeField] public Sprite EmptySlotSprite;
    }
}