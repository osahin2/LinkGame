using App;
using System;
using UnityEngine;
namespace UI
{
    public abstract class UIView : MonoBehaviour, IUIView
    {
        [SerializeField] private ViewTypes _viewType;

        protected IGameContext GameContext;
        public ViewTypes ViewType => _viewType;
        public void Construct(IGameContext gameContext)
        {
            GameContext = gameContext;
        }
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
