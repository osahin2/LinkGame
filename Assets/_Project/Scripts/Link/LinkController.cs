using App;
using Extensions;
using Grid;
using Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Link
{
    public class LinkController : ILinkController
    {
        public event Action<IEnumerable<IGridSlot>> OnItemsLinked;

        private readonly IInputSystem _inputSystem;
        private readonly IGameBoard _gameBoard;
        private readonly LineDrawer _lineDrawer;
        public int MinLinkCount { get; }

        private LinkedList<IGridSlot> _linkedItems = new();
        private LinkedListNode<IGridSlot> _currentNode = null;
        private int _linkCounter;
        private bool _canDrag;
        public LinkController(IGameContext gameContext)
        {
            _gameBoard = gameContext.Locator.Get<IGameBoard>();
            _inputSystem = gameContext.Locator.Get<IInputSystem>();
            _lineDrawer = gameContext.Locator.Get<LineDrawer>();
            MinLinkCount = gameContext.Locator.Get<LinkData>().LinkCount;
        }
        public void Init()
        {
            AddEvents();
        }
        public void DeInit()
        {
            RemoveEvents();
        }
        private void AddNewLink(IGridSlot gridSlot)
        {
            var previousNode = _currentNode;

            if (!previousNode.Value.GridPosition.CheckIfNeighbour(gridSlot.GridPosition))
            {
                return;
            }

            if (_currentNode.Value == gridSlot)
            {
                return;
            }

            if (_currentNode.Value.Item.ID == gridSlot.Item.ID && !_linkedItems.Contains(gridSlot))
            {
                _lineDrawer.DrawNewLine(previousNode.Value.GridPosition, gridSlot.GridPosition);

                _currentNode = _linkedItems.AddLast(gridSlot);
                _linkCounter++;
                gridSlot.Item.Select();
            }
        }
        private void RemoveLastLinked(IGridSlot gridSlot)
        {
            if (_currentNode.Previous == null)
                return;

            if (_currentNode.Previous.Value == gridSlot)
            {
                _currentNode.Value.Item.DeSelect();
                _currentNode = _currentNode.Previous;
                _linkedItems.RemoveLast();
                _linkCounter--;
                _lineDrawer.EraseLastLine();
            }
        }
        private bool TryGetGridSlot(Vector2 worldPos, out IGridSlot gridSlot)
        {
            var gridPos = _gameBoard.WorldToGrid(worldPos);
            gridSlot = null;

            if (!gridPos.IsPositionOnGrid(_gameBoard.Width, _gameBoard.Height))
                return false;
            gridSlot = _gameBoard[gridPos];
            return true;
        }
        private void ResetAllSelectedItems()
        {
            foreach (var slot in _linkedItems)
            {
                slot.Item.DeSelect();
            }
        }
        private void Reset()
        {
            _linkedItems.Clear();
            _currentNode = null;
            _linkCounter = 0;
            _lineDrawer.ResetLineRenderer();
        }
        private void OnPointerDownHandler(object sender, InputEventArgs args)
        {
            if (!TryGetGridSlot(args.WorldPosition, out var gridSlot))
            {
                return;
            }
            _canDrag = true;
            _currentNode = _linkedItems.AddFirst(gridSlot);
            _linkCounter++;
            gridSlot.Item.Select();
            _lineDrawer.SetPosition(_gameBoard.GridToWorldCenter(gridSlot.GridPosition));
        }
        private void OnPointerDragHandler(object sender, InputEventArgs args)
        {
            if (!_canDrag)
            {
                return;
            }
            if (!TryGetGridSlot(args.WorldPosition, out var gridSlot))
            {
                return;
            }

            AddNewLink(gridSlot);
            RemoveLastLinked(gridSlot);
        }
        private void OnPointerUpHandler(object sender, InputEventArgs args)
        {
            if (_linkCounter >= MinLinkCount)
            {
                OnItemsLinked?.Invoke(_linkedItems);
            }
            else
            {
                ResetAllSelectedItems();
            }
            _canDrag = false;
            Reset();
        }
        private void AddEvents()
        {
            _inputSystem.PointerDown += OnPointerDownHandler;
            _inputSystem.PointerDrag += OnPointerDragHandler;
            _inputSystem.PointerUp += OnPointerUpHandler;
        }
        private void RemoveEvents()
        {
            _inputSystem.PointerDown -= OnPointerDownHandler;
            _inputSystem.PointerDrag -= OnPointerDragHandler;
            _inputSystem.PointerUp -= OnPointerUpHandler;
        }
    }
}
