using System;
using UnityEngine;
namespace UI
{
    public abstract class UIView : MonoBehaviour, IUIView
    {
        [SerializeField] private ViewTypes _viewType;

        public ViewTypes ViewType => _viewType;
        public virtual void Show()
        {
            AddEvents();
        }
        public virtual void Hide()
        {
            RemoveEvents();
        }
        protected abstract void AddEvents();
        protected abstract void RemoveEvents();
    }
}
