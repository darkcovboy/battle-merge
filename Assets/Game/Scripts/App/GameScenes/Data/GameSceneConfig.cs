using UnityEngine;

namespace Game.Scripts.App.GameScenes.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class GameSceneConfig : ScriptableObject
    {
        [Header("Progression")]
        [Min(1)] public int StartLevel = 1;
        [Min(1)] public int BossLevelInterval = 5;
        [Min(1)] public int TotalUniqueLevels = 20;

        [Header("Scaling")]
        [Range(1f, 3f)] public float CycleDifficultyMultiplier = 1.15f;
    }
}