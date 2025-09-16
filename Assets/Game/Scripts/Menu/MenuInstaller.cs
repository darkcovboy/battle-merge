using Game.Scripts.App.Save;
using Game.Scripts.Menu.BuyButtons;
using Game.Scripts.Menu.MenuInput;
using Game.Scripts.Modules.Tutorial;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        
        [SerializeField] private BuyButtonCharacterView[] _buyButtonCharacterViews;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
            Container.BindInterfacesAndSelfTo<Field.Field>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuyButtonCharacterPresenter>().AsSingle().WithArguments(_buyButtonCharacterViews);
            MenuInputInstaller.Install(Container,_mainCamera);
            SaveLoadMenuInstaller.Install(Container);
        }
    }
}