using Game.Scripts.App.Save;
using Game.Scripts.Infrastructure.Loader;
using Game.Scripts.Menu.BuyButtons;
using Game.Scripts.Menu.CharacterUnlock;
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
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuyButtonCharacterPresenter>().AsSingle().WithArguments(_buyButtonCharacterViews);
            Container.BindInterfacesAndSelfTo<CharacterUnlockService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEventMediator>().AsSingle();
            MenuInputInstaller.Install(Container,_mainCamera);
            SaveLoadMenuInstaller.Install(Container);
        }
    }
}