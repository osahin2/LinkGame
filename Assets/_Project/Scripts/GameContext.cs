using BoardSolvers;
using Grid;
using Inputs;
using Item.Data;
using Item.Factory;
using Link;
using Service_Locator;
using System.Collections.Generic;
using UnityEngine;
namespace App
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private ItemsContainer _itemContainer;
        [SerializeField] private List<ItemPoolFactory> _itemPoolFactories = new();
        [field: SerializeField] public Gameplay Gameplay { get; private set;}

        private IItemFactory _itemFactory;
        private ILinkSolver _linkSolver;
        private IFill _fallDownFill;
        private BoardSolver _boardSolver;

        public void Construct()
        {
            _itemFactory = new RecyclableItemFactory(_itemPoolFactories);
            _linkSolver = new LinkSolver(_gameBoard, _inputSystem);
            _fallDownFill = new FallDownFill(_gameBoard, _itemFactory, _itemContainer);
            _boardSolver = new BoardSolver(_linkSolver, _fallDownFill, _itemFactory);

            Gameplay.Construct(_gameBoard, _itemFactory, _itemContainer, _boardSolver);
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
