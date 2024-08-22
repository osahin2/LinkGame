namespace Tile.Factory
{
    public interface ITileFactory
    {
        IGridTile Get(TileState state);
        void Release(IGridTile tile);
    }
}
