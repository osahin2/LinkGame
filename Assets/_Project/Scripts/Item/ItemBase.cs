using DG.Tweening;
using Item.Data;
using System;
using UnityEngine;
namespace Item
{
    public abstract class ItemBase : MonoBehaviour, IItem
    {
        [SerializeField] private ItemType _itemType;

        protected ItemData ItemData;
        private ItemSettings Settings => ItemData.Settings;
        public Transform Transform => transform;
        public ItemType Type => _itemType;
        public int ID {  get; private set; }

        private Tween _scaleUpTween;

        public virtual void SetItemData(ItemData itemData)
        {
            ItemData = itemData;
            ID = itemData.ID;
        }
        public void Show()
        {
            ResetItem();
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

        public virtual void Select()
        {
            _scaleUpTween = transform.DOScale(Vector3.one * Settings.LinkedScale, Settings.LinkedAnimTime)
                .SetEase(Ease.OutBack);
        }
        public virtual void DeSelect()
        {
            _scaleUpTween?.Kill();
            transform.DOScale(Vector3.one, Settings.LinkedAnimTime)
                .SetEase(Ease.InBack);
        }

        public virtual void Pop(Action onComplete = null)
        {
            _scaleUpTween?.Kill();
            transform.DOScale(Vector3.zero, Settings.LinkedAnimTime)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }
        private void ResetItem()
        {
            transform.localScale = Vector3.one;
        }

    }
}
