using UnityEngine;
namespace Item
{
    public class LinkItem : MonoBehaviour, IItem
    {
        [SerializeField] private SpriteRenderer _renderer;
        public Transform Transform => transform;

        public void SetSprite(Sprite icon)
        {
            _renderer.sprite = icon;
        }
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
