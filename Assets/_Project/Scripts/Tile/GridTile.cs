using UnityEngine;
namespace Tile
{
    public abstract class GridTile : MonoBehaviour, IGridTile
    {
        public abstract TileState State { get; }
        public abstract bool CanContainItem { get; }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
