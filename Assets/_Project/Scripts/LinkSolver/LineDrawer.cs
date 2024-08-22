using App;
using Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Link
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private List<LineDirectionInfo> _directionInfos = new();

        private int PosLastIndex => _lineRenderer.positionCount - 1;

        private readonly Dictionary<Directions, Vector2> _directionDict = new();

        public void Construct()
        {
            foreach (var info in _directionInfos)
            {
                _directionDict.Add(info.Direction, info.DirectionVector);
            }
        }
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        public void DrawNewLine(Vector2Int grid, Vector2Int neighbour)
        {
            _lineRenderer.positionCount++;
            var previousIndexPos = _lineRenderer.GetPosition(PosLastIndex - 1);
            var direction = FindDirection(grid, neighbour);
            _lineRenderer.SetPosition(PosLastIndex, previousIndexPos + GetDirection(direction));
        }
        public void EraseLastLine()
        {
            _lineRenderer.positionCount--;
        }
        public void ResetLineRenderer()
        {
            _lineRenderer.positionCount = 1;
        }
        private Vector3 GetDirection(Directions direction)
        {
            if(!_directionDict.TryGetValue(direction, out var directionVector))
            {
                throw new KeyNotFoundException($"LineDrawer: {direction} Not Found In Dictionary");
            }
            return directionVector;
        }
        private Directions FindDirection(Vector2Int gridPos, Vector2Int neighbour)
        {
            var gridDirections = GetDirections(gridPos);
            foreach (var direction in gridDirections)
            {
                var vector = direction.vector;
                if (vector == neighbour)
                {
                    return direction.directionEnum;
                }
            }
            Debug.LogError("LineDrawer: Direction Not Found");
            return Directions.None;
        }
        private List<(Vector2Int vector, Directions directionEnum)> GetDirections(Vector2Int gridPos)
        {
            return new()
            {
                (gridPos.Up(), Directions.Up),
                (gridPos.Down(), Directions.Down),
                (gridPos.Right(), Directions.Right),
                (gridPos.Left(), Directions.Left),
                (gridPos.UpLeft(), Directions.UpLeft),
                (gridPos.DownLeft(), Directions.DownLeft),
                (gridPos.UpRight(), Directions.UpRight),
                (gridPos.DownRight(), Directions.DownRight),
            };
        }

        [Serializable]
        private struct LineDirectionInfo
        {
            public Directions Direction;
            public Vector2 DirectionVector;
        }
        [Serializable]
        private enum Directions
        {
            None,
            Right,
            Left,
            Up,
            Down,
            UpRight,
            UpLeft,
            DownRight,
            DownLeft
        }
    }
}
