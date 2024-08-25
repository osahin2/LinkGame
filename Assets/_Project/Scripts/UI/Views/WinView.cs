using DG.Tweening;
using Event;
using EventBusSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinView : UIView
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private float _buttonScaleTime = .25f;

        public override void Show()
        {
            _nextButton.transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            _nextButton.transform.DOScale(Vector3.one, _buttonScaleTime)
                .SetEase(Ease.OutBack);
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
            _nextButton.transform.DOScale(Vector3.zero, _buttonScaleTime)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
        private void OnNextClicked()
        {
            EventBus<Events.NextLevel>.TriggerEvent(Events.NEXT_LEVEL, new Events.NextLevel());
        }
        protected override void AddEvents()
        {
            _nextButton.onClick.AddListener(OnNextClicked);
        }

        protected override void RemoveEvents()
        {
            _nextButton.onClick.RemoveListener(OnNextClicked);
        }
    }
}
