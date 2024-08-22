using Factory;
using UnityEngine;
namespace Item.Factory
{

    [CreateAssetMenu(fileName = "ItemPoolFactory", menuName = "Item/Item Pool Factory")]
    public class ItemPoolFactory : MonoPoolFactory<ItemBase, IItem>
    {
        public ItemType ItemType => Prefab.Type;

        protected override void OnConstructed()
        {
            Pooler = GetPoolerBuilder()
                .WithOnInitialSpawn(OnInitiallySpawned)
                .Build();
        }

        private void OnInitiallySpawned(IItem item)
        {
            item.Hide();
        }
    }
}
