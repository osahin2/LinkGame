using Grid;
using Item.Factory;
using Link;
using System.Collections.Generic;
namespace BoardSolvers
{
    public class BoardSolver
    {
        private ILinkSolver _linkSolver;
        private IFill _fallDownFill;
        private IFill _onSetFill;
        private IItemFactory _itemFactory;
        public BoardSolver(ILinkSolver linkSolver, IFill fill, IItemFactory itemFactory, IFill onSetFill)
        {
            _linkSolver = linkSolver;
            _fallDownFill = fill;
            _itemFactory = itemFactory;
            _onSetFill = onSetFill;
        }
        public void Init()
        {
            _linkSolver.Init();
            _onSetFill.Fill();
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
            _fallDownFill.Fill(gridSlots);
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
