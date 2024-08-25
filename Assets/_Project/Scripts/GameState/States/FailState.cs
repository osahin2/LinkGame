using App;
using Event;
using EventBusSystem;
using UI;
namespace GameStates
{
    public class FailState : IGameState
    {
        private readonly Gameplay _gameplay;
        private readonly IUIManager _uiManager;
        private readonly GameManager _gameManager;
        public GameState State => GameState.Fail;
        public FailState(Gameplay gameplay, IUIManager uiManager, GameManager gameManager)
        {
            _gameplay = gameplay;
            _uiManager = uiManager;
            _gameManager = gameManager;
        }
        public void Init()
        {
            EventBus<Events.RestartLevel>.RegisterEvent(Events.LEVEL_FAILED, OnRestartClicked);
            _uiManager.ShowView(ViewTypes.Fail);
        }
        public void DeInit()
        {
            EventBus<Events.RestartLevel>.DeRegisterEvent(Events.NEXT_LEVEL, OnRestartClicked);
        }
        private void OnRestartClicked(Events.RestartLevel args)
        {
            _uiManager.HideView(ViewTypes.Fail);
            _uiManager.AlphaTransition.Transition(1f, onFade: () =>
            {
                _gameplay.DeInit();
                _gameManager.ChangeState(GameState.Play);
            });
        }
    }
}
