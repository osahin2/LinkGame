using DG.Tweening;
using Event;
using EventBusSystem;
using Level;
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

        private ILevel LevelController => _levelController ??= GameContext.Locator.Get<ILevel>();
        private ILevel _levelController;
        private LevelData LevelData => LevelController.GetLevelData();

        private Tween _moveTextScaleTween;
        private Tween _scoreTween;
        public override void Show()
        {
            base.Show();
            SetMoveText(LevelData.MoveCount);
            SetScoreText(LevelData.Score);
            gameObject.SetActive(true);
        }
        public override void Hide()
        {
            base.Hide();
            gameObject.SetActive(false);
        }
        private void MoveTextChange(int value)
        {
            _moveTextScaleTween?.Kill(true);
            _moveTextScaleTween = _moveText.transform.DOPunchScale(Vector3.one * _moveTextAnimScale, _scaleAnimTime);
            SetMoveText(value);
        }
        private void SetMoveText(int value)
        {
            _moveText.text = value.ToString();
        }
        private void SetScoreText(int value)
        {
            _scoreText.text = value.ToString();
        }
        private void ScoreTextChangeTween(int oldScore, int newScore)
        {
            _scoreTween?.Kill(true);
            _scoreTween = DOVirtual.Int(oldScore, newScore, _scoreDecreaseTime, SetScoreText);
        }
        private void OnLinkCompletedHandler(Events.LinkCompleted args)
        {
            MoveTextChange(args.MoveCount);
            ScoreTextChangeTween(args.OldScore, args.NewScore);
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
