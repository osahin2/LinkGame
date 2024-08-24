using Extensions;
using Grid;
using Item.Factory;
using Link;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BoardSolvers
{
    public class BoardSolver
    {
        private ILinkController _linkController;
        private IFill _fallDownFill;
        private IFill _onSetFill;
        private IItemFactory _itemFactory;
        private IGameBoard _gameBoard;
        private BoardShuffler _boardShuffler;

        private HashSet<IGridSlot>[] _links;
        private int _matchIndex;

        public BoardSolver(IGameBoard gameBoard, ILinkController linkSolver, IFill fill, IItemFactory itemFactory, IFill onSetFill)
        {
            _linkController = linkSolver;
            _fallDownFill = fill;
            _itemFactory = itemFactory;
            _onSetFill = onSetFill;
            _gameBoard = gameBoard;
            _boardShuffler = new BoardShuffler(gameBoard);
        }
        public void Init()
        {
            _linkController.Init();
            _onSetFill.Fill();
            LinkCheckAndShuffleRecursive();
            AddEvents();
        }
        public void DeInit()
        {
            RemoveEvents();
            _linkController.DeInit();
        }
        
        private void ClearLinkedItems(IEnumerable<IGridSlot> gridSlots)
        {
            foreach (var slot in gridSlots)
            {
                var item = slot.Item;
                slot.Clear();
                item.Pop(onComplete:() =>
                {
                    item.Hide();
                    _itemFactory.Release(item);
                });
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
                    if (_links[currentIndex].Count >= _linkController.MinLinkCount)
                    {
                        return true;
                    }

                }
            }
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
        private List<Vector2Int> SearchDirections(Vector2Int slot)
        {
            return new() { slot.Up(), slot.Right(), slot.UpRight(), slot.DownRight() };
        }
        private void OnFillCompleted()
        {
            LinkCheckAndShuffleRecursive();
        }
        private void OnItemsLinkedHandler(IEnumerable<IGridSlot> gridSlots)
        {
            ClearLinkedItems(gridSlots);
            _fallDownFill.Fill(gridSlots, OnFillCompleted);
        }
        private void AddEvents()
        {
            _linkController.OnItemsLinked += OnItemsLinkedHandler;
        }
        private void RemoveEvents()
        {
            _linkController.OnItemsLinked -= OnItemsLinkedHandler;
        }
    }
}
