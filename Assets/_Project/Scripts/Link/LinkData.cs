using UnityEngine;
namespace Link
{
    [CreateAssetMenu(fileName = "LinkData", menuName = "Link/Link Data")]
    public class LinkData : ScriptableObject
    {
        [field: SerializeField] public int LinkCount { get; private set; } = 3;
        [field: SerializeField] public int PerLinkMultiplier { get; private set; } = 5;
    }
}
