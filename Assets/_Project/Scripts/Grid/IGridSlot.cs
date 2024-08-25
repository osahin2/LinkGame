using Item;
using Tile;
using UnityEngine;
namespace Grid
{
    public interface IGridSlot
    {
        Vector2Int GridPosition { get; }
        IItem Item { get; }
        IGridTile Tile { get; }
        bool HasItem { get; }

        void SetItem(IItem item);
        void SetTile(IGridTile tile);
        void Clear();
    }
}
