using App;
using UI;
using UnityEngine;
namespace GameStates
{
    public class PlayState : IGameState
    {
        private readonly IUIManager _uiManager;
        private readonly Gameplay _gameplay;
        private readonly GameManager _gameManager;

        public GameState State => GameState.Play;
        public PlayState(Gameplay gameplay, GameManager gameManager, IUIManager uiManager)
        {
            _gameplay = gameplay;
            _gameManager = gameManager;
            _uiManager = uiManager;
        }
        public void Init()
        {
            _gameplay.Init();
            _uiManager.ShowView(ViewTypes.InGame);
            _uiManager.AlphaTransition.Transition(0f);

            _gameplay.OnLevelFail += OnLevelFail;
            _gameplay.OnLevelWin += OnLevelWin;
        }
        public void DeInit()
        {
            _gameplay.OnLevelFail -= OnLevelFail;
            _gameplay.OnLevelWin -= OnLevelWin;
        }
        private void OnLevelFail()
        {
            _gameManager.ChangeState(GameState.Fail);
        }
        private void OnLevelWin()
        {
            _gameManager.ChangeState(GameState.Win);
        }
    }
}
