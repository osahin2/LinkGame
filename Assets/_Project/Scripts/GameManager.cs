using GameStates;
using Level;
using System.Collections.Generic;
using UI;
using UnityEngine;
namespace App
{
    public class GameManager : MonoBehaviour
    {
        private Gameplay _gameplay;
        private IUIManager _uiManager;
        private LevelController _levelController;

        private List<IGameState> _gameStates = new();
        private IGameState _currentGameState;
        
        public void Construct(Gameplay gameplay, IUIManager uiManager, LevelController level)
        {
            Application.targetFrameRate = 60;

            _gameplay = gameplay;
            _uiManager = uiManager;
            _levelController = level;
            ConstructGameStates();
        }
        private void Start()
        {
            ChangeState(GameState.Play);
        }
        public void ChangeState(GameState state)
        {
            _currentGameState?.DeInit();
            _currentGameState = _gameStates.Find(x => x.State == state);
            _currentGameState?.Init();
        }
        private void ConstructGameStates()
        {
            _gameStates = new()
            {
                new WinState(_uiManager, _gameplay, _levelController, this),
                new FailState(_gameplay, _uiManager, this),
                new PlayState(_gameplay, this, _uiManager)
            };
        }
    }
}
