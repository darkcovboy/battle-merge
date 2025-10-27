using Game.Scripts.Battler.FortniteLikeZone;
using Game.Scripts.Battler.StateGame;
using Game.Scripts.Battler.UI.BattleShower;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Battle
{
    public class BattlerInstaller : MonoInstaller
    {
        [SerializeField] private BattleShowerView _view;
        [SerializeField] private BattleZoneScaler _battleZoneScaler;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleZoneScaler>().FromInstance(_battleZoneScaler).AsSingle();
            
            Container.Bind<MatchStateService>().AsSingle().NonLazy();
            
            Container.Bind<BattleShowerView>().FromInstance(_view).AsSingle();

            Container.Bind<BattlerShowerPresenter>().AsSingle();
            
            Container.Bind<BattleController>().AsSingle().NonLazy();
        }
    }
}