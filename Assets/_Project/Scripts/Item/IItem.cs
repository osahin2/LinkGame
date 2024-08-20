using UnityEngine;
namespace Item
{
    public interface IItem
    {
        Transform Transform { get; }
        void SetSprite(Sprite icon);
        void Show();
        void Hide();
        void SetPosition(Vector3 position);
    }
}
