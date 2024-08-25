using Item.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level/Level Data")]
    public class LevelData : ScriptableObject
    {
        public List<ItemData> FallItems = new();
        public List<LevelGridData> GridDatas = new();
        public int Score;
        public int MoveCount;
        public int GridWidth;
        public int GridHeight;

        private readonly Dictionary<Vector2Int, LevelGridData> _gridDataDict = new();

        public void Construct()
        {
            foreach (var data in GridDatas)
            {
                _gridDataDict.Add(data.GridPosition, data);
            }
        }
        [ContextMenu(nameof(CreateRandomLevel))]
        private void CreateRandomLevel()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    var randomFallItem = FallItems[Random.Range(0, FallItems.Count)];
                    var pos = new Vector2Int(x, y);
                    var newGridData = new LevelGridData { GridPosition = pos, ItemData = randomFallItem};
                    GridDatas.Add(newGridData);
                }
            }
        }
        public LevelGridData GetGridData(Vector2Int gridPosition)
        {
            if (_gridDataDict.TryGetValue(gridPosition, out LevelGridData data))
            {
                return data;
            }
            throw new KeyNotFoundException($"{GetType().Name}: {gridPosition} Grid Not Found In Dictionary");
        }
    }
    [Serializable]
    public struct LevelGridData
    {
        public ItemData ItemData;
        public Vector2Int GridPosition;
    }
}
