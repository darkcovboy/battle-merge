using UnityEngine;

namespace Game.Scripts.App.GameScenes.GameReward.Data
{
    [CreateAssetMenu(fileName = "RewardConfig", menuName = "Configs/RewardConfig")]
    public class RewardConfig : ScriptableObject
    {
        [Header("Coins")]
        [Min(0)] public int BaseRewardPerEnemy = 10;
        [Range(1f, 3f)] public float EnemyRewardMultiplierPerCycle = 1.1f;

        [Header("Gems")]
        [Min(0)] public int BossGemsReward = 5;
    }
}