namespace GameStates
{
    public interface IGameState
    {
        public GameState State { get; }

        void Init();
        void DeInit();
    }
}
