using DG.Tweening;
using Event;
using EventBusSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InGameView : UIView
    {
        [SerializeField] private TextMeshProUGUI _moveText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private float _moveTextAnimScale = 1.05f;
        [SerializeField] private float _scaleAnimTime = .25f;
        [SerializeField] private float _scoreDecreaseTime = .35f;

        private Tween _moveTextScaleTween;
        private Tween _scoreTween;
        private void SetMoveText(int value)
        {
            _moveTextScaleTween?.Kill(true);
            _moveTextScaleTween = _moveText.transform.DOPunchScale(Vector3.one * _moveTextAnimScale, _scaleAnimTime);
            _moveText.text = value.ToString();
        }
        private void SetScoreText(int oldScore, int newScore)
        {
            _scoreTween?.Kill(true);
            _scoreTween =  DOVirtual.Int(oldScore, newScore, _scoreDecreaseTime, SetText);

            void SetText(int value)
            {
                _scoreText.text = value.ToString();
            }
        }
        private void OnLinkCompletedHandler(Events.LinkCompleted args)
        {
            SetMoveText(args.MoveCount);
            SetScoreText(args.OldScore, args.NewScore);
        }
        protected override void AddEvents()
        {
            EventBus<Events.LinkCompleted>.RegisterEvent(Events.LINK_COMPLETED, OnLinkCompletedHandler);
        }

        protected override void RemoveEvents()
        {
            EventBus<Events.LinkCompleted>.DeRegisterEvent(Events.LINK_COMPLETED, OnLinkCompletedHandler);
        }
    }
}
