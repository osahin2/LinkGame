using System.Collections.Generic;
using UnityEngine;
namespace Tile.Factory
{
    public class RecyclableTileFactory : ITileFactory
    {
        private readonly List<TilePoolFactory> _tileFactories = new();

        private readonly Dictionary<TileState, TilePoolFactory> _factoriesDict = new();

        private readonly Transform _parent;
        public RecyclableTileFactory(List<TilePoolFactory> factories)
        {
            _tileFactories = factories;
            _parent = new GameObject("TilesParent").transform;

            ConstructAndAddFactoriesToDict();
        }
        private void ConstructAndAddFactoriesToDict()
        {
            foreach (var factory in _tileFactories)
            {
                factory.Construct(_parent);
                _factoriesDict.Add(factory.State, factory);
            }
        }
        public IGridTile Get(TileState state)
        {
            return GetFactory(state).Get();
        }

        public void Release(IGridTile tile)
        {
            GetFactory(tile.State).Free(tile);
        }
        private TilePoolFactory GetFactory(TileState state)
        {
            if(!_factoriesDict.TryGetValue(state, out TilePoolFactory factory))
            {
                throw new KeyNotFoundException($"RecyclableTileFactory: {state} Not Found In Factory Dictionary");
            }
            return factory;
        }
    }
}
