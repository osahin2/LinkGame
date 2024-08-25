using App;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private List<UIView> _uiScreens = new();
        [SerializeField] private AlphaTransition _alphaTransition;

        private IGameContext _gameContext;

        public AlphaTransition AlphaTransition => _alphaTransition;

        private readonly Dictionary<ViewTypes, UIView> _viewDict = new();

        public void Construct(IGameContext gameContext)
        {
            _gameContext = gameContext;
            SetViewDictionary();
        }
        private void SetViewDictionary()
        {
            foreach (var view in _uiScreens)
            {
                view.Construct(_gameContext);
                _viewDict.Add(view.ViewType, view);
            }
        }
        public void ShowView(ViewTypes viewTypes)
        {
            GetViewFromDict(viewTypes).Show();
        }
        public void HideView(ViewTypes viewTypes)
        {
            GetViewFromDict(viewTypes).Hide();
        }
        private UIView GetViewFromDict(ViewTypes view)
        {
            if (!_viewDict.TryGetValue(view, out var screen))
            {
                throw new KeyNotFoundException($"{GetType().Name}: {view} View Not Found In Dictionary");
            }
            return screen;
        }
    }
}
