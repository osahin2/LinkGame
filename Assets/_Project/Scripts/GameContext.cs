using BoardSolvers;
using Grid;
using Inputs;
using Item.Data;
using Item.Factory;
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
        [field: SerializeField] public Gameplay Gameplay { get; private set;}

        [Header("Data")]
        [SerializeField] private ItemsContainer _itemContainer;

        [Header("Item Factories")]
        [SerializeField] private List<ItemPoolFactory> _itemPoolFactories = new();
        [Header("Tile Factories")]
        [SerializeField] private List<TilePoolFactory> _tilePoolFactories = new();

        private IItemFactory _itemFactory;
        private ITileFactory _tileFactory;
        private ILinkSolver _linkSolver;
        private IFill _fallDownFill;
        private IFill _onSetFill;
        private BoardSolver _boardSolver;

        public void Construct()
        {
            _itemFactory = new RecyclableItemFactory(_itemPoolFactories);
            _tileFactory = new RecyclableTileFactory(_tilePoolFactories);
            _linkSolver = new LinkSolver(_gameBoard, _inputSystem);
            _fallDownFill = new FallDownFill(_gameBoard, _itemFactory, _itemContainer);
            _onSetFill = new OnSetFill(_gameBoard, _itemFactory, _tileFactory, _itemContainer);
            _boardSolver = new BoardSolver(_linkSolver, _fallDownFill, _itemFactory, _onSetFill);

            Gameplay.Construct(_gameBoard, _boardSolver);
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
