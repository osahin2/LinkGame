using UnityEngine;

namespace Extensions
{
    public static class GridExtensions
    {
        public static Vector2Int Left(this Vector2Int value)
        {
            return value + Vector2Int.left;
        }
        public static Vector2Int Right(this Vector2Int value)
        {
            return value + Vector2Int.right;
        }
        public static Vector2Int Up(this Vector2Int value)
        {
            return value + Vector2Int.up;
        }
        public static Vector2Int Down(this Vector2Int value)
        {
            return value + Vector2Int.down;
        }
        public static bool IsPositionOnGrid(this Vector2Int gridPosition, int width, int heigth)
        {
            return gridPosition.x >= 0 &&
                   gridPosition.x < width &&
                   gridPosition.y >= 0 &&
                   gridPosition.y < heigth;
        }
    }
}
