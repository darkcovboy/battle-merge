using Game.Scripts.Battler.UI.Lose;
using Game.Scripts.Battler.UI.Win;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private LoseScreenView _loseScreenView;
        [SerializeField] private WinScreenView _winScreenView;

        public override void InstallBindings()
        {
            Container.Bind<LoseScreenView>().FromInstance(_loseScreenView).AsSingle();
            Container.Bind<WinScreenView>().FromInstance(_winScreenView).AsSingle();

            //Container.BindInterfacesAndSelfTo<WinScreenPresenter>().AsSingle().NonLazy();
           // Container.BindInterfacesAndSelfTo<LoseScreenPresenter>().AsSingle().NonLazy();
        }
    }
}