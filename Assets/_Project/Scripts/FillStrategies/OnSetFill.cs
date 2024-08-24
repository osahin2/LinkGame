using Grid;
using Item;
using Item.Data;
using Item.Factory;
using Level;
using System;
using System.Collections.Generic;
using Tile.Factory;
namespace BoardSolvers
{
    public class OnSetFill : IFill
    {
        private IGameBoard _gameBoard;
        private IItemFactory _itemFactory;
        private ITileFactory _tileFactory;
        private ILevel _level;
        public OnSetFill(IGameBoard gameBoard, IItemFactory itemFactory, ITileFactory tileFactory, ILevel level)
        {
            _gameBoard = gameBoard;
            _itemFactory = itemFactory;
            _tileFactory = tileFactory;
            _level = level;
        }
        public void Fill(IEnumerable<IGridSlot> solvedGrids, Action onCompleted)
        {
            for (int row = 0; row < _gameBoard.Width; row++)
            {
                for (int column = 0; column < _gameBoard.Height; column++)
                {
                    var slot = _gameBoard[row, column];
                    var levelData = _level.GetLevelData();
                    var levelGridData = levelData.GetGridData(slot.GridPosition);
                    var item = _itemFactory.Get(levelGridData.ItemData.Type);
                    var tile = _tileFactory.Get(Tile.TileState.Available);
                    var targetPos = _gameBoard.GridToWorldCenter(slot.GridPosition);

                    tile.SetPosition(targetPos);
                    tile.Show();

                    item.SetPosition(targetPos);
                    item.SetItemData(levelGridData.ItemData);
                    slot.SetItem(item);
                    item.Show();
                }
            }
        }
    }
}
