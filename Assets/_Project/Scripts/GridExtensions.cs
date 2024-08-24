using System.Collections.Generic;
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
        public static Vector2Int UpRight(this Vector2Int value)
        {
            return value + (Vector2Int.up + Vector2Int.right);
        }
        public static Vector2Int UpLeft(this Vector2Int value)
        {
            return value + (Vector2Int.left + Vector2Int.up);
        }
        public static Vector2Int DownRight(this Vector2Int value)
        {
            return value + (Vector2Int.right + Vector2Int.down);
        }
        public static Vector2Int DownLeft(this Vector2Int value)
        {
            return value + (Vector2Int.left + Vector2Int.down);
        }
        public static List<Vector2Int> GetDirections(this Vector2Int value)
        {
            return new()
            {
                value.Up(),
                value.Down(),
                value.Left(),
                value.Right(),
                value.UpLeft(),
                value.DownLeft(),
                value.UpRight(),
                value.DownRight(),
            };
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
