using Grid;
using Item;
using Item.Data;
using Item.Factory;
using System.Collections.Generic;
using Tile.Factory;
namespace BoardSolvers
{
    public class OnSetFill : IFill
    {
        private IGameBoard _gameBoard;
        private IItemFactory _itemFactory;
        private ITileFactory _tileFactory;
        private ItemsContainer _itemContainer;
        public OnSetFill(IGameBoard gameBoard, IItemFactory itemFactory, ITileFactory tileFactory, ItemsContainer container)
        {
            _gameBoard = gameBoard;
            _itemFactory = itemFactory;
            _tileFactory = tileFactory;
            _itemContainer = container;
        }
        public void Fill(IEnumerable<IGridSlot> solvedGrids)
        {
            for (int row = 0; row < _gameBoard.Width; row++)
            {
                for (int column = 0; column < _gameBoard.Height; column++)
                {
                    var slot = _gameBoard[row, column];
                    var itemData = _itemContainer.GetRandomItemData();
                    var item = _itemFactory.Get(ItemType.LinkItem);
                    var tile = _tileFactory.Get(Tile.TileState.Available);
                    var targetPos = _gameBoard.GridToWorldCenter(slot.GridPosition);

                    tile.SetPosition(targetPos);
                    tile.Show();

                    item.SetPosition(targetPos);
                    item.SetItemData(itemData);
                    slot.SetItem(item);
                    item.Show();
                }
            }
        }
    }
}
