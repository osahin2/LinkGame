using App;
using Extensions;
using Grid;
using Item.Factory;
using Link;
using System.Collections.Generic;
using Tile.Factory;
using UnityEngine;
namespace BoardSolvers
{
    public class BoardSolver
    {
        private readonly IFill _fallDownFill;
        private readonly IFill _onSetFill;
        private readonly IItemFactory _itemFactory;
        private readonly ITileFactory _tileFactory;
        private readonly IGameBoard _gameBoard;
        private readonly BoardShuffler _boardShuffler;

        private HashSet<IGridSlot>[] _links;
        private int _matchIndex;
        private readonly int _minLinkCount;

        public BoardSolver(IGameContext gameContext)
        {
            _fallDownFill = new FallDownFill(gameContext);
            _onSetFill = new OnSetFill(gameContext);
            _itemFactory = gameContext.Locator.Get<IItemFactory>();
            _gameBoard = gameContext.Locator.Get<IGameBoard>();
            _boardShuffler = new BoardShuffler(_gameBoard);
            _tileFactory = gameContext.Locator.Get<ITileFactory>();
            _minLinkCount = gameContext.Locator.Get<LinkData>().LinkCount;
        }
        public void Init()
        {
            _onSetFill.Fill();
            LinkCheckAndShuffleRecursive();
        }
        public void DeInit()
        {
            ClearAllItems();
        }
        public void FillBoard(IEnumerable<IGridSlot> gridSlots)
        {
            ClearLinkedItems(gridSlots);
            _fallDownFill.Fill(gridSlots, OnFillCompleted);
        }
        private void ClearLinkedItems(IEnumerable<IGridSlot> gridSlots)
        {
            foreach (var slot in gridSlots)
            {
                var item = slot.Item;
                slot.Clear();
                item.Pop(onComplete: () =>
                {
                    item.Hide();
                    _itemFactory.Release(item);
                });
            }
        }
        private void ClearAllItems()
        {
            foreach (var slot in _gameBoard.GridSlots1D)
            {
                slot.Item.Hide();
                slot.Tile.Hide();
                _tileFactory.Release(slot.Tile);
                _itemFactory.Release(slot.Item);
                slot.Clear();
            }
        }
        private bool AnyLinkOnBoard()
        {
            _matchIndex = 0;
            _links = new HashSet<IGridSlot>[_gameBoard.Width * _gameBoard.Height];

            foreach (var slot in _gameBoard.GridSlots1D)
            {
                foreach (var direction in SearchDirections(slot.GridPosition))
                {
                    if (!direction.IsPositionOnGrid(_gameBoard.Width, _gameBoard.Height))
                    {
                        continue;
                    }

                    if (_gameBoard[direction].Item.ID != slot.Item.ID)
                    {
                        continue;
                    }

                    int currentIndex;

                    if (AnyExistInLinks(_gameBoard[direction], out var index))
                    {
                        AddLink(slot.GridPosition, direction, index);
                        currentIndex = index;
                    }
                    else
                    {
                        AddLink(slot.GridPosition, direction, _matchIndex);
                        currentIndex = _matchIndex;
                        _matchIndex++;
                    }

                    if (_links[currentIndex].Count >= _minLinkCount)
                    {
                        return true;
                    }

                }
            }
            return false;
        }
        private bool AnyExistInLinks(IGridSlot gridSlot, out int index)
        {
            for (int i = 0; i < _links.Length; i++)
            {
                HashSet<IGridSlot> set = _links[i];

                if (set == null)
                {
                    continue;
                }

                foreach (var slot in set)
                {
                    if (!gridSlot.GridPosition.CheckIfNeighbour(slot.GridPosition))
                    {
                        continue;
                    }

                    if (slot.Item.ID != gridSlot.Item.ID)
                    {
                        continue;
                    }

                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }
        private void AddLink(Vector2Int current, Vector2Int next, int index)
        {
            if (_links[index] == null)
            {
                _links[index] = new HashSet<IGridSlot>();
            }
            _links[index].Add(_gameBoard[current]);
            _links[index].Add(_gameBoard[next]);
        }
        private void LinkCheckAndShuffleRecursive()
        {
            if (!AnyLinkOnBoard())
            {
                _boardShuffler.Shuffle(onShuffled: () =>
                {
                    LinkCheckAndShuffleRecursive();
                });
            }
        }
        private void OnFillCompleted()
        {
            LinkCheckAndShuffleRecursive();
        }
        private List<Vector2Int> SearchDirections(Vector2Int slot)
        {
            return new() { slot.Up(), slot.Right(), slot.UpRight(), slot.DownRight() };
        }
    }
}
