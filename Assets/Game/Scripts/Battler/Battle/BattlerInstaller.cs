using Game.Scripts.Battler.UI.BattleShower;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Battle
{
    public class BattlerInstaller : MonoInstaller
    {
        [SerializeField] private BattleShowerView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<BattleShowerView>().FromInstance(_view).AsSingle();

            Container.Bind<BattlerShowerPresenter>().AsSingle();
            
            Container.Bind<BattleController>().AsSingle().NonLazy();
        }
    }
}