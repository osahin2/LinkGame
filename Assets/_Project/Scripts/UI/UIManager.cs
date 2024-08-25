using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [SerializeField] private List<UIView> _uiScreens = new();
        [SerializeField] private AlphaTransition _alphaTransition;

        public AlphaTransition AlphaTransition => _alphaTransition;

        private readonly Dictionary<ViewTypes, UIView> _screenDict = new();

        public void Construct()
        {
            SetScreenDictionary();
        }
        private void SetScreenDictionary()
        {
            foreach (var screen in _uiScreens)
            {
                _screenDict.Add(screen.ViewType, screen);
            }
        }
        public void ShowView(ViewTypes state)
        {
            GetViewFromDict(state).Show();
        }
        public void HideView(ViewTypes state)
        {
            GetViewFromDict(state).Hide();
        }
        private UIView GetViewFromDict(ViewTypes state)
        {
            if (!_screenDict.TryGetValue(state, out var screen))
            {
                throw new KeyNotFoundException($"{GetType().Name}: Screen Not Found In Dictionary");
            }
            return screen;
        }
    }
}
