using Grid;
using Item;
using Item.Data;
using Item.Factory;
using Link;
using UnityEngine;
namespace App
{
    public class Gameplay : MonoBehaviour
    {
        private GameBoard _gameBoard;
        private ItemsContainer _itemContainer;
        private IItemFactory _itemFactory;
        private LinkController _linkController;
        public void Construct(GameBoard gameBoard, IItemFactory itemFactory, ItemsContainer itemContainer)
        {
            _gameBoard = gameBoard;
            _itemFactory = itemFactory;
            _itemContainer = itemContainer;
        }
        public void Init()
        {
            _linkController = new LinkController();

            _gameBoard.Init();
            FillBoard();
            _linkController.Init();
        }
        public void DeInit()
        {

        }
        private void FillBoard()
        {
            for (int row = 0; row < _gameBoard.Width; row++)
            {
                for (int column = 0; column < _gameBoard.Height; column++)
                {
                    var slot = _gameBoard[row, column];
                    var itemData = _itemContainer.GetRandomItemData();
                    var item = _itemFactory.Get(ItemType.LinkItem);
                    item.SetPosition(_gameBoard.GridToWorldCenter(slot.GridPosition));
                    item.SetItemData(itemData);
                    slot.SetItem(item);
                    item.Show();
                }
            }
        }
    }
}
