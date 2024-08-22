using Factory;
using UnityEngine;
namespace Tile.Factory
{
    [CreateAssetMenu(fileName = "TilePoolFactory", menuName = "Tile/Tile Pool Factory")]
    public class TilePoolFactory : MonoPoolFactory<GridTile, IGridTile>
    {
        public TileState State => Prefab.State;
        protected override void OnConstructed()
        {
            Pooler = GetPoolerBuilder()
                .WithOnInitialSpawn(OnInitiallySpawned)
                .Build();
        }
        private void OnInitiallySpawned(IGridTile gridTile)
        {
            gridTile.Hide();
        }
    }
}
