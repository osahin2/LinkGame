using Item.Data;
using System;
using UnityEngine;
namespace Item
{
    public class LinkItem : ItemBase
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private SpriteRenderer _glowRenderer;
        public override void SetItemData(ItemData itemData)
        {
            base.SetItemData(itemData);
            _renderer.sprite = itemData.Icon;
            _glowRenderer.sprite = itemData.Icon;
        }
        public override void Select()
        {
            base.Select();
            _glowRenderer.gameObject.SetActive(true);
        }
        public override void DeSelect()
        {
            base.DeSelect();
            _glowRenderer.gameObject.SetActive(false);
        }
        public override void Pop(Action onComplete = null)
        {
            base.Pop(onComplete);
            _glowRenderer.gameObject.SetActive(false);
        }
    }
}
