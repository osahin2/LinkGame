using Grid;
using Item.Factory;
using Link;
using System.Collections.Generic;
namespace BoardSolvers
{
    public class BoardSolver
    {
        private ILinkSolver _linkSolver;
        private IFill _fillSolver;
        private IItemFactory _itemFactory;
        public BoardSolver(ILinkSolver linkSolver, IFill fill, IItemFactory itemFactory)
        {
            _linkSolver = linkSolver;
            _fillSolver = fill;
            _itemFactory = itemFactory;
        }
        public void Init()
        {
            _linkSolver.Init();
            AddEvents();
        }
        public void DeInit()
        {
            RemoveEvents();
            _linkSolver.DeInit();
        }
        private void ClearLinkedItems(IEnumerable<IGridSlot> gridSlots)
        {
            foreach (var slot in gridSlots)
            {
                var item = slot.Item;
                slot.Clear();
                _itemFactory.Release(item);
                item.Hide();
            }
        }
        private void OnItemsLinkedHandler(IEnumerable<IGridSlot> gridSlots)
        {
            ClearLinkedItems(gridSlots);
            _fillSolver.Fill(gridSlots);
        }
        private void AddEvents()
        {
            _linkSolver.OnItemsLinked += OnItemsLinkedHandler;
        }
        private void RemoveEvents()
        {
            _linkSolver.OnItemsLinked -= OnItemsLinkedHandler;
        }
    }
}
