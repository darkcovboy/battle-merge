using System;
using Game.Scripts.App.GameScenes.Data;
using UnityEngine;

namespace Game.Scripts.App.GameScenes
{
    public class GameSceneManager
    {
        private readonly GameSceneConfig _config;
        private int _currentLevel;
        private int _uiLevel;


        public int CurrentUILevel => _uiLevel;
        public int CurrentLevel => _currentLevel;
        public int CycleIndex => (_currentLevel - 1) / _config.TotalUniqueLevels;
        public int RealLevelIndex => ((_currentLevel - 1) % _config.TotalUniqueLevels) + 1;
        public bool IsBossLevel => _currentLevel % _config.BossLevelInterval == 0;

        public GameSceneManager(GameSceneConfig config)
        {
            _config = config;
        }

        public void Setup(int currentIndex, int currentUILevel)
        {
            _uiLevel = currentUILevel;
            _currentLevel = currentIndex;
        }
        
        public void CompleteLevel()
        {
            _currentLevel++;
            _uiLevel++;
        }

        public float GetCycleMultiplier()
        {
            return Mathf.Pow(_config.CycleDifficultyMultiplier, CycleIndex);
        }
    }
}