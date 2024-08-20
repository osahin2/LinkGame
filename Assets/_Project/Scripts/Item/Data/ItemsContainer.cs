using System.Collections.Generic;
using UnityEngine;
namespace Item.Data
{
    [CreateAssetMenu(fileName = "ItemContainer", menuName = "Item/Item Container")]
    public class ItemsContainer : ScriptableObject
    {
        [SerializeField] private List<ItemData> _items = new();

        private readonly Dictionary<int, ItemData> _itemsDict = new();

        public void Construct()
        {
            SetDictionary();
        }
        private void SetDictionary()
        {
            foreach (ItemData item in _items)
            {
                _itemsDict.Add(item.ID, item);
            }
        }
        public ItemData GetItemDataById(int id)
        {
            if(!_itemsDict.TryGetValue(id, out ItemData item))
            {
                throw new KeyNotFoundException($"ItemsContainer: {id} ID not found in Item Dictionary");
            }
            return item;
        }
        public ItemData GetRandomItemData()
        {
            return _items[Random.Range(0, _items.Count)];
        }
    }
}
