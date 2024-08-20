using Item.Data;
using UnityEngine;
namespace Item
{
    public abstract class ItemBase : MonoBehaviour, IItem
    {
        [SerializeField] private ItemType _itemType;

        protected ItemData ItemData;
        public Transform Transform => transform;
        public ItemType Type => _itemType;

        public virtual void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
        }
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
