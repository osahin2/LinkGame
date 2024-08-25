using BoardSolvers;
using Grid;
using Inputs;
using Item.Data;
using Item.Factory;
using Level;
using Link;
using Service_Locator;
using System.Collections.Generic;
using Tile.Factory;
using UI;
using UnityEngine;
namespace App
{
    public class GameContext : MonoBehaviour, IGameContext
    {
        [Header("Scene References")]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private LineDrawer _lineDrawer;
        [SerializeField] private UIManager _uiManager;

        [Header("Data")]
        [SerializeField] private ItemsContainer _itemContainer;
        [SerializeField] private List<LevelData> _levelDatas = new();
        [SerializeField] private LinkData _linkData;

        [Header("Item Factories")]
        [SerializeField] private List<ItemPoolFactory> _itemPoolFactories = new();
        [Header("Tile Factories")]
        [SerializeField] private List<TilePoolFactory> _tilePoolFactories = new();

        public IServiceLocator Locator { get; private set; }

        private Gameplay _gameplay;
        private BoardSolver _boardSolver;
        private LevelController _levelController;

        private void Awake()
        {
            Locator = new ServiceLocator();

            _itemContainer.Construct();
            _lineDrawer.Construct();
            _uiManager.Construct(this);
            _levelController = new LevelController(_levelDatas);

            RegisterInstances();

            _boardSolver = new BoardSolver(this);
            _gameplay = new Gameplay(_gameBoard, _boardSolver, this);

            _gameManager.Construct(_gameplay, _uiManager, _levelController);
        }
        private void RegisterInstances()
        {
            Locator.Register<ILevel>(_levelController);
            Locator.Register<IGameBoard>(_gameBoard);
            Locator.Register<IItemFactory>(new RecyclableItemFactory(_itemPoolFactories));
            Locator.Register<ITileFactory>(new RecyclableTileFactory(_tilePoolFactories));
            Locator.Register<IInputSystem>(_inputSystem);
            Locator.Register(_linkData);
            Locator.Register(_itemContainer);
            Locator.Register(_lineDrawer);
        }
    }
}
