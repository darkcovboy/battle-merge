using Game.Scripts.App.GameScenes.GameReward.Data;
using UnityEngine;

namespace Game.Scripts.App.GameScenes.GameReward
{
    public class RewardManager
    {
        private readonly RewardConfig _config;
        private readonly GameSceneManager _gameSceneManager;

        public RewardManager(RewardConfig config, GameSceneManager gameSceneManager)
        {
            _config = config;
            _gameSceneManager = gameSceneManager;
        }
        
        public RewardData CalculateLevelReward(float enemyCount)
        {
            var cycle = _gameSceneManager.CycleIndex;
            var difficultyMultiplier = _gameSceneManager.GetCycleMultiplier();

            float coinMultiplier = Mathf.Pow(_config.EnemyRewardMultiplierPerCycle, cycle);
            int coins = Mathf.RoundToInt(
                _config.BaseRewardPerEnemy * enemyCount * difficultyMultiplier * coinMultiplier
            );

            int gems = 0;
            if (_gameSceneManager.IsBossLevel)
            {
                gems = _config.BossGemsReward;
            }

            return new RewardData(coins, gems);
        }
    }
    public readonly struct RewardData
    {
        public readonly int Coins;
        public readonly int Gems;

        public RewardData(int coins, int gems)
        {
            Coins = coins;
            Gems = gems;
        }
    }
}