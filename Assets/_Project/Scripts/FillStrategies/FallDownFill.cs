using DG.Tweening;
using Extensions;
using Grid;
using Item.Data;
using Item.Factory;
using Level;
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
        private ILevel _level;

        public FallDownFill(IGameBoard gameBoard, IItemFactory itemFactory, ItemsContainer container, ILevel level)
        {
            _gameBoard = gameBoard;
            _itemFactory = itemFactory;
            _itemsContainer = container;
            _level = level;
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
                    fallSeq.Join(item.Transform.DOMove
                        (_gameBoard.GridToWorldCenter(targetPos),
                            distance / _itemsContainer.Settings.FallSpeed)
                        .SetEase(_itemsContainer.Settings.FallEase));
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
                    var levelData = _level.GetLevelData();
                    var randomItemData = levelData.FallItems[Random.Range(0, levelData.FallItems.Count)];
                    var item = _itemFactory.Get(randomItemData.Type);
                    var spawnGridPosition = new Vector2Int(grid.GridPosition.x, _gameBoard.Height);
                    var itemSpawnPos = _gameBoard.GridToWorldCenter(spawnGridPosition);

                    item.SetPosition(itemSpawnPos);
                    item.SetItemData(randomItemData);
                    item.Show();
                    emptyGrid.SetItem(item);

                    var distance = spawnGridPosition.y - emptyGrid.GridPosition.y;
                    fillSeq.AppendCallback(() => item.Transform.DOMove
                        (_gameBoard.GridToWorldCenter(emptyGrid.GridPosition),
                            distance / _itemsContainer.Settings.FallSpeed)
                        .SetEase(_itemsContainer.Settings.FallEase))
                        .AppendInterval(_itemsContainer.Settings.FillInterval);
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
            if (targetGrid == null || targetGrid.HasItem)
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
            for (int i = 0; i < _gameBoard.Height; i++)
            {
                var targetSlot = _gameBoard[slot.GridPosition.x, i];
                if (!targetSlot.HasItem)
                    emptySlots.Add(targetSlot);
            }
            return emptySlots;
        }
    }
}
