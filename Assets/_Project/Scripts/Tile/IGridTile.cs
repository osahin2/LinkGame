
using UnityEngine;

namespace Tile
{
    public interface IGridTile
    {
        TileState State { get; }
        public bool CanContainItem { get; }
        void Show();
        void Hide();
        void SetPosition(Vector3 position);
    }
}
