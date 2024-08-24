using System.Collections.Generic;
namespace Level
{
    public class LevelController : ILevel
    {
        private List<LevelData> _levelDatas = new();

        private int _currentLevelIndex;

        public LevelController(List<LevelData> levelDatas)
        {
            _levelDatas = levelDatas;

            foreach (var data in _levelDatas)
            {
                data.Construct();
            }
        }
        public LevelData GetLevelData()
        {
            return _levelDatas[_currentLevelIndex];
        }
        public void IncreaseLevel()
        {
            _currentLevelIndex++;
            if(_currentLevelIndex >= _levelDatas.Count)
            {
                _currentLevelIndex = 0;
            }
        }
    }
}
