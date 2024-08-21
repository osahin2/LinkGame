using DG.Tweening;
using Extensions;
using Grid;
using Item.Data;
using Item.Factory;
using Item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BoardSolvers
{
    public class FallDownFill : IFill
    {
        private IGameBoard _gameBoard;
        private IItemFactory _itemFactory;
        private ItemsContainer _itemsContainer;

        public FallDownFill(IGameBoard gameBoard, IItemFactory itemFactory, ItemsContainer container)
        {
            _gameBoard = gameBoard;
            _itemFactory = itemFactory;
            _itemsContainer = container;
        }
        public void Fill(IEnumerable<IGridSlot> solvedGrids)
        {
            FallItems(solvedGrids);
            FillNewItems(solvedGrids);
        }
        private void FallItems(IEnumerable<IGridSlot> solvedGrids)
        {
            var fallSeq = DOTween.Sequence();
            foreach (var grid in solvedGrids)
            {
                for (var i = 0; i < _gameBoard.Height; i++)
                {
                    var gridSlot = _gameBoard[grid.GridPosition.x, i];
                    if (!gridSlot.HasItem)
                    {
                        continue;
                    }
                    if (!CanDropDown(gridSlot, out var targetPos))
                    {
                        continue;
                    }
                    var item = gridSlot.Item;
                    gridSlot.Clear();
                    _gameBoard[targetPos].SetItem(item);
                    var distance = gridSlot.GridPosition.y - targetPos.y;
                    fallSeq.Join(
                        item.Transform.DOMove(_gameBoard.GridToWorldCenter(targetPos), distance / 12f));
                }
            }
        }
        private void FillNewItems(IEnumerable<IGridSlot> solvedGrids)
        {
            var seqList = new List<Sequence>();

            foreach (var grid in solvedGrids)
            {
                var fillSeq = DOTween.Sequence();
                fillSeq.Pause();
                seqList.Add(fillSeq);

                var emptySlots = FindEmptySlotsOnColumn(grid);

                foreach (var emptyGrid in emptySlots)
                {
                    var randomItemData = _itemsContainer.GetRandomItemData();
                    var item = _itemFactory.Get(ItemType.LinkItem);
                    var spawnGridPosition = new Vector2Int(grid.GridPosition.x, _gameBoard.Height);
                    var itemSpawnPos = _gameBoard.GridToWorldCenter(spawnGridPosition);

                    item.SetPosition(itemSpawnPos);
                    item.SetItemData(randomItemData);
                    item.Show();
                    emptyGrid.SetItem(item);

                    var distance = spawnGridPosition.y - emptyGrid.GridPosition.y;
                    fillSeq.AppendCallback(()=>
                        item.Transform.DOMove(_gameBoard.GridToWorldCenter(emptyGrid.GridPosition), distance / 12f)
                        .SetEase(Ease.Linear))
                        .AppendInterval(.1f);
                }
            }
            foreach (var seq in seqList)
            {
                seq.Play();
            }
        }
        private bool CanDropDown(IGridSlot slot, out Vector2Int pos)
        {
            var targetGrid = slot;
            while (IsMoveable(targetGrid, out var gridPos))
            {
                targetGrid = _gameBoard[gridPos];
            }
            pos = targetGrid.GridPosition;
            return targetGrid != slot;
        }
        private bool IsMoveable(IGridSlot slot, out Vector2Int pos)
        {
            var targetGrid = _gameBoard.GetGridSlot(slot.GridPosition.Down());
            if(targetGrid == null || targetGrid.HasItem)
            {
                pos = Vector2Int.zero;
                return false;
            }
            pos = targetGrid.GridPosition;
            return true;
        }
        private List<IGridSlot> FindEmptySlotsOnColumn(IGridSlot slot)
        {
            var emptySlots = new List<IGridSlot>();
            for (int i = _gameBoard.Height - 1; i >= 0; i--)
            {
                var targetSlot = _gameBoard[slot.GridPosition.x, i];
                if (!targetSlot.HasItem)
                    emptySlots.Add(targetSlot);
            }
            emptySlots.Reverse();
            return emptySlots;
        }
    }
}
