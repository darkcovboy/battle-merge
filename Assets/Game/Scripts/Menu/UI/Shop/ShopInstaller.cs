using Game.Scripts.Menu.UI.Shop.Data;
using Game.Scripts.Menu.UI.Shop.ModelsDisplay;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.UI.Shop
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private HeroCardView _prefab;
        [SerializeField] private ShopView _shopView;
        [SerializeField] private HeroViewConfigHolder _config;
        [SerializeField] private SkinPlacement _skinPlacement;

        public override void InstallBindings()
        {
            Container.Bind<HeroCardView>().FromInstance(_prefab).AsSingle();
            Container.Bind<ShopView>().FromInstance(_shopView).AsSingle();
            Container.Bind<HeroViewConfigHolder>().FromInstance(_config).AsSingle();
            Container.Bind<SkinPlacement>().FromInstance(_skinPlacement).AsSingle();
            Container.BindInterfacesAndSelfTo<ShopPresenter>().AsSingle();
        }
    }
}