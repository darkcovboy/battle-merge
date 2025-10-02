using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.UI.CharacterInfo.Cards
{
    public class CardsInstaller : MonoInstaller
    {
        [SerializeField] private CardsView _cardsView;
        [SerializeField] private CardCellView _prefab;
        
        public override void InstallBindings()
        {
            Container.Bind<CardsView>().FromInstance(_cardsView).AsSingle();
            Container.Bind<CardCellView>().FromInstance(_prefab).AsSingle();
            
            Container.Bind<CardsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CardsPresenter>().AsSingle().NonLazy();
        }
    }
}