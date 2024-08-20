using Extensions;
using Grid;
using Inputs;
using Item;
using Service_Locator;
using System.Collections.Generic;
using UnityEngine;

namespace Link
{
    public class LinkController
    {
        private IInputSystem _inputSystem;
        private IGameBoard _gameBoard;

        private LinkedList<IItem> _linkedItems = new();
        private LinkedListNode<IItem> _currentNode = null;

        private int _minLinkCount = 3;
        private int _linkCounter;

        public LinkController()
        {
            ServiceProvider.Instance
                .Get(out _inputSystem)
                .Get(out _gameBoard);
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
            if (_currentNode.Value == gridSlot.Item)
            {
                return;
            }

            if (_currentNode.Value.ID == gridSlot.Item.ID && !_linkedItems.Contains(gridSlot.Item))
            {
                _currentNode = _linkedItems.AddLast(gridSlot.Item);
                _linkCounter++;
            }
        }
        private void RemoveLastLinked(IGridSlot gridSlot)
        {
            if (_currentNode.Previous == null)
                return;

            if (_currentNode.Previous.Value == gridSlot.Item)
            {
                _currentNode = _currentNode.Previous;
                _linkedItems.RemoveLast();
                _linkCounter--;
            }
        }
        private void ClearLinkedNodes()
        {
            foreach (var item in _linkedItems)
            {
                item.Hide();
            }
        }
        private void Reset()
        {
            _linkedItems.Clear();
            _currentNode = null;
            _linkCounter = 0;
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
        private void OnPointerDownHandler(object sender, InputEventArgs args)
        {
            if (!TryGetGridSlot(args.WorldPosition, out var gridSlot))
            {
                return;
            }
            _currentNode = _linkedItems.AddFirst(gridSlot.Item);
            _linkCounter++;
        }
        private void OnPointerDragHandler(object sender, InputEventArgs args)
        {
            if (!TryGetGridSlot(args.WorldPosition, out var gridSlot))
            {
                return;
            }

            AddNewLink(gridSlot);
            RemoveLastLinked(gridSlot);
        }
        private void OnPointerUpHandler(object sender, InputEventArgs args)
        {
            if (_linkCounter >= _minLinkCount)
            {
                ClearLinkedNodes();
            }
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
