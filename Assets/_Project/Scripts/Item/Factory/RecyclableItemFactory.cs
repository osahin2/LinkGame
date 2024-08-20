using System.Collections.Generic;
using UnityEngine;
namespace Item.Factory
{
    public class RecyclableItemFactory : IItemFactory
    {
        private readonly List<ItemPoolFactory> _itemFactories = new();

        private readonly Dictionary<ItemType, ItemPoolFactory> _factoriesDict = new();

        private readonly Transform _parent;

        public RecyclableItemFactory(List<ItemPoolFactory> factories)
        {
            _itemFactories = factories;
            _parent = new GameObject("ItemsParent").transform;

            ConstructAndAddFactoriesToDict();
        }
        private void ConstructAndAddFactoriesToDict()
        {
            foreach (var factory in _itemFactories)
            {
                factory.Construct(_parent);
                _factoriesDict.Add(factory.ItemType, factory);
            }
        }
        public IItem Get(ItemType itemType)
        {
            return GetItemFactory(itemType).Get();
        }
        public void Release(IItem item)
        {
            GetItemFactory(item.Type).Free(item);
        }
        private ItemPoolFactory GetItemFactory(ItemType itemType)
        {
            if (!_factoriesDict.TryGetValue(itemType, out var factory))
            {
                throw new KeyNotFoundException($"{itemType} Not Found In Factory Dictionary");
            }
            return factory;
        }
    }
}
