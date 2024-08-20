using Item;
using UnityEngine;
namespace Grid
{
    public interface IGridSlot
    {
        Vector2Int GridPosition { get; }
        IItem Item { get; }
        bool HasItem { get; }

        void SetItem(IItem item);
        void Clear();
    }
}
