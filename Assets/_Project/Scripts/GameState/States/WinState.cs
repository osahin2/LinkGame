using App;
using Event;
using EventBusSystem;
using Level;
using UI;
namespace GameStates
{
    public class WinState : IGameState
    {
        private readonly IUIManager _uiManager;
        private readonly GameManager _gameManager;
        private readonly LevelController _levelController;
        private readonly Gameplay _gameplay;
        public GameState State => GameState.Win;

        public WinState(IUIManager uiManager, Gameplay gameplay, LevelController level, GameManager gameManager)
        {
            _uiManager = uiManager;
            _gameplay = gameplay;
            _levelController = level;
            _gameManager = gameManager;
        }
        public void Init()
        {
            _uiManager.ShowView(ViewTypes.Win);

            EventBus<Events.NextLevel>.RegisterEvent(Events.NEXT_LEVEL, OnNextLevelClicked);
        }
        public void DeInit()
        {
            EventBus<Events.NextLevel>.DeRegisterEvent(Events.NEXT_LEVEL, OnNextLevelClicked);
        }
        private void OnNextLevelClicked(Events.NextLevel args)
        {
            _uiManager.HideView(ViewTypes.Win);
            _uiManager.AlphaTransition.Transition(1f, onFade: () =>
            {
                _gameplay.DeInit();
                _levelController.IncreaseLevel();
                _gameManager.ChangeState(GameState.Play);
            });
        }
    }
}
