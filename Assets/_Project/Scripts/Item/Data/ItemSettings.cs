using UnityEngine;
namespace Item.Data
{
    [CreateAssetMenu(fileName = "ItemSettings", menuName = "Item/Item Settings")]
    public class ItemSettings : ScriptableObject
    {
        [field: SerializeField] public float LinkedScale { get; private set; } = 1.2f;
        [field: SerializeField] public float LinkedAnimTime { get; private set; } = .25f;
        [field: SerializeField] public float FallSpeed { get; private set; } = 11f;
        [field: SerializeField] public AnimationCurve FallEase { get; private set; }
        [field: SerializeField] public float FillInterval { get; private set; } = .1f;
    }
}
