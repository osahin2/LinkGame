using Item;
using Tile;
using UnityEngine;
namespace Grid
{
    public class GridSlot : IGridSlot
    {
        public Vector2Int GridPosition { get; }

        public IItem Item { get; private set; }
        public IGridTile Tile {  get; private set; }
        public bool HasItem => Item != null;


        public GridSlot(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
        }

        public void SetItem(IItem item)
        {
            if(item != null)
            {
                Item = item;
            }
        }
        public void SetTile(IGridTile tile)
        {
            if (tile != null)
            {
                Tile = tile;
            }
        }
        public void Clear()
        {
            Item = null;
        }

    }
}
