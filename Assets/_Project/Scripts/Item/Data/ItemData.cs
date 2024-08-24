using UnityEngine;
namespace Item.Data
{
    [CreateAssetMenu(fileName = "Item Data", menuName ="Item/Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public ItemType Type { get; private set; }
        public ItemSettings Settings { get; private set; }
        public int ID => GetInstanceID();

        public void Construct(ItemSettings settings)
        {
            Settings = settings;
        }
    }
}
