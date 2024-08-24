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
using UnityEngine;
namespace App
{
    public class GameContext : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private LineDrawer _lineDrawer;
        [field: SerializeField] public Gameplay Gameplay { get; private set; }

        [Header("Data")]
        [SerializeField] private ItemsContainer _itemContainer;
        [SerializeField] private List<LevelData> _levelDatas = new();
        [SerializeField] private int _minLinkCount;

        [Header("Item Factories")]
        [SerializeField] private List<ItemPoolFactory> _itemPoolFactories = new();
        [Header("Tile Factories")]
        [SerializeField] private List<TilePoolFactory> _tilePoolFactories = new();

        private IItemFactory _itemFactory;
        private ITileFactory _tileFactory;
        private ILinkController _linkController;
        private IFill _fallDownFill;
        private IFill _onSetFill;
        private BoardSolver _boardSolver;
        private LevelController _levelController;

        public void Construct()
        {
            _itemContainer.Construct();
            _lineDrawer.Construct();

            _levelController = new LevelController(_levelDatas);
            _itemFactory = new RecyclableItemFactory(_itemPoolFactories);
            _tileFactory = new RecyclableTileFactory(_tilePoolFactories);
            _linkController = new LinkController(_gameBoard, _inputSystem, _lineDrawer, _minLinkCount);
            _fallDownFill = new FallDownFill(_gameBoard, _itemFactory, _itemContainer, _levelController);
            _onSetFill = new OnSetFill(_gameBoard, _itemFactory, _tileFactory, _levelController);
            _boardSolver = new BoardSolver(_gameBoard, _linkController, _fallDownFill, _itemFactory, _onSetFill);

            Gameplay.Construct(_gameBoard, _boardSolver, _levelController);
        }
        public void RegisterInstances()
        {
            ServiceProvider.Instance
                .Register<IGameBoard>(_gameBoard)
                .Register<IInputSystem>(_inputSystem)
                .Register(_itemFactory);
        }
    }
}
