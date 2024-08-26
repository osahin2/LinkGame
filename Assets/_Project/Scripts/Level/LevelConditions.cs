using Event;
using EventBusSystem;
using Level;
using System;
namespace App
{
    public class LevelConditions
    {
        public event Action OnMoveReachedZero;
        public event Action OnScoreReachedZero;

        private ILevel _levelController;
        public bool IsScoreCompleted { get; private set; }

        private int _moveCount;
        private int _scoreCount;
        private int _targetScore;
        public LevelConditions(ILevel level)
        {
            _levelController = level;
        }
        public void Init()
        {
            var levelData = _levelController.GetLevelData();
            _moveCount = levelData.MoveCount;
            _targetScore = levelData.Score;
            _scoreCount = 0;
            IsScoreCompleted = false;
        }
        public void SetConditions(int score)
        {
            var oldScore = _scoreCount;
            IncreaseScore(score);
            DecreaseMove();

            EventBus<Events.LinkCompleted>.TriggerEvent(Events.LINK_COMPLETED,
                new Events.LinkCompleted(_scoreCount, oldScore, _moveCount));
        }
        private void DecreaseMove()
        {
            _moveCount--;
            if (_moveCount == 0)
            {
                OnMoveReachedZero?.Invoke();
            }
        }
        private void IncreaseScore(int score)
        {
            if (IsScoreCompleted)
            {
                return;
            }
            if (_scoreCount + score >= _targetScore)
            {
                IsScoreCompleted = true;
                _scoreCount = _targetScore;
                OnScoreReachedZero?.Invoke();
                return;
            }
            _scoreCount += score;
        }
    }
}
