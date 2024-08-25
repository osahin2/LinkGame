using DG.Tweening;
using Event;
using EventBusSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FailView : UIView
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private float _buttonScaleTime = .25f;

        public override void Show()
        {
            _restartButton.transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            _restartButton.transform.DOScale(Vector3.one, _buttonScaleTime)
                .SetEase(Ease.OutBack);
            base.Show();
        }
        public override void Hide()
        {
            base.Hide();
            _restartButton.transform.DOScale(Vector3.zero, _buttonScaleTime)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
        private void OnRestartClicked()
        {
            EventBus<Events.RestartLevel>.TriggerEvent(Events.RESTART_LEVEL, new Events.RestartLevel());
        }
        protected override void AddEvents()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        protected override void RemoveEvents()
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }
    }
}
