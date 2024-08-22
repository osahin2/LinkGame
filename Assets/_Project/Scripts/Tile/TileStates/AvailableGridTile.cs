using UnityEngine;
namespace Tile
{
    public class AvailableGridTile : GridTile
    {
        public override TileState State => TileState.Available;
        public override bool CanContainItem => true;

    }
}
