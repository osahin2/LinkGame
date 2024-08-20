using Grid;
using Inputs;
using Item.Data;
using Item.Factory;
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

        private RecyclableItemFactory _itemFactory;

        public void Construct()
        {
            _itemFactory = new RecyclableItemFactory(_itemPoolFactories);
            Gameplay.Construct(_gameBoard, _itemFactory, _itemContainer);
        }
        public void RegisterInstances()
        {
            ServiceProvider.Instance
                .Register<IGameBoard>(_gameBoard)
                .Register<IInputSystem>(_inputSystem)
                .Register<IItemFactory>(_itemFactory);
        }
    }
}
