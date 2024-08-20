using Item.Data;
using UnityEngine;
namespace Item
{
    public interface IItem
    {
        Transform Transform { get; }
        ItemType Type { get; }
        int ID {  get; }
        void SetItemData(ItemData itemData);
        void Show();
        void Hide();
        void SetPosition(Vector3 position);
    }
}
