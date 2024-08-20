using UnityEngine;
namespace Grid
{
    public interface IGameBoard
    {
        public IGridSlot this[int row, int column] { get; }
        public IGridSlot this[Vector2Int position] { get; }
        public int Width { get; }
        public int Height { get; }

        IGridSlot GetGridSlot(Vector2Int position);
        Vector3 GridToWorldCenter(Vector2Int gridPosition);
        Vector2Int WorldToGrid(Vector3 worldPosition);
    }
}
