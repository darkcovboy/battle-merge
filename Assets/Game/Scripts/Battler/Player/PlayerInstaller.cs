using Game.Scripts.Battler.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Scripts.Battler.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (Application.isMobilePlatform)
            {
                Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
            }
        }
    }
}