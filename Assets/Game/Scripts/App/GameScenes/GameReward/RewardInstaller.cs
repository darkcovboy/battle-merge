using Game.Scripts.App.GameScenes.GameReward.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.GameScenes.GameReward
{
    [CreateAssetMenu(
        fileName = "RewardInstaller",
        menuName = "Zenject/New RewardInstaller"
    )]
    public class RewardInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private RewardConfig _rewardConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<RewardManager>().AsSingle().WithArguments(_rewardConfig).NonLazy();
        }
    }
}