using Extensions;
using UnityEngine;
namespace Grid
{
    public class GameBoard : IGameBoard
    {
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private float _slotSize;

        private IGridSlot[,] _gridSlots;

        public int Width => _gridWidth;
        public int Height => _gridHeight;
        public IGridSlot this[int width, int heigth] => _gridSlots[width, heigth];
        public IGridSlot this[Vector2Int position] => _gridSlots[position.x, position.y];

        private Vector3 _origin;

        public void Init(int width = 0, int heigth = 0)
        {
            _gridWidth = width == 0 ? _gridWidth : width;
            _gridHeight = heigth == 0 ? _gridHeight : heigth;

            _gridSlots = new IGridSlot[_gridWidth, _gridHeight];
            CreateGrid();
        }
        public void DeInit()
        {
            _gridSlots = null;
        }
        private void CreateGrid()
        {
            var originX = _gridWidth / 2f * _slotSize;
            var originY = _gridHeight / 2f * _slotSize;

            _origin = Vector3.zero - new Vector3(originX, originY);

            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    var gridSlot = new GridSlot(new Vector2Int(x, y));
                    _gridSlots[x, y] = gridSlot;
                }
            }
        }
        private Vector3 GridToWorld(Vector2Int gridPosition)
        {
            return (new Vector3(gridPosition.x, gridPosition.y) * _slotSize) + _origin;
        }
        private Vector3 GridToWorld(int x, int y)
        {
            return (new Vector3(x, y) * _slotSize) + _origin;
        }
        public Vector3 GridToWorldCenter(Vector2Int gridPosition)
        {
            return GridToWorld(gridPosition) + new Vector3(_slotSize, _slotSize) * .5f;
        }
        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt((worldPosition - _origin).x / _slotSize);
            var y = Mathf.FloorToInt((worldPosition - _origin).y / _slotSize);

            return new Vector2Int(x, y);
        }
        public IGridSlot GetGridSlot(Vector2Int position)
        {
            return position.IsPositionOnGrid(_gridWidth, _gridHeight) ? this[position] : null;
        }
    }
}
