using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.MenuInput
{
    public class MenuInputInstaller : Installer<Camera, MenuInputInstaller>
    {
        [Inject] private Camera _mainCamera;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DesktopMenuInput>().AsSingle().WithArguments(_mainCamera);
            Container.BindInterfacesAndSelfTo<Raycaster>().AsSingle().WithArguments(_mainCamera).NonLazy();
        }
    }
}