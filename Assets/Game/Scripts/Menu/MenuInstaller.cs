using Game.Scripts.App.Save;
using Game.Scripts.Menu.MenuInput;
using Game.Scripts.Modules.Tutorial;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
            SaveLoadMenuInstaller.Install(Container);
            MenuInputInstaller.Install(Container,_mainCamera);
        }
    }
}