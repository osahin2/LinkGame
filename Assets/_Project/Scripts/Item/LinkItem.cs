using Item.Data;
using UnityEngine;
namespace Item
{
    public class LinkItem : ItemBase
    {
        [SerializeField] private SpriteRenderer _renderer;
        public override void SetItemData(ItemData itemData)
        {
            base.SetItemData(itemData);
            _renderer.sprite = itemData.Icon;
        }
    }
}
