using UnityEngine;
namespace App
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;

        private Gameplay _gamePlay => _gameContext.Gameplay;

        public void Awake()
        {
            Application.targetFrameRate = 60;

            _gameContext.Construct();
            _gameContext.RegisterInstances();
        }
        private void Start()
        {
            _gamePlay.Init();
        }
    }
}
