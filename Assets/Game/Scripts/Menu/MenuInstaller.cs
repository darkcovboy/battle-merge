using Game.Scripts.App.Save;
using Game.Scripts.Modules.Tutorial;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
            SaveLoadMenuInstaller.Install(Container);
        }
    }
}