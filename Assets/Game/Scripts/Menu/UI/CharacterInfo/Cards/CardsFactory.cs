using Game.Scripts.App.Characters.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.UI.CharacterInfo.Cards
{
    public class CardsFactory : IFactory<Transform, bool, CharacterConfig, CardCellView>
    {
        private readonly CardCellView _prefab;

        public CardsFactory(CardCellView prefab)
        {
            _prefab = prefab;
        }
        public CardCellView Create(Transform position, bool isUnlocked, CharacterConfig config)
        {
            CardCellView view = Object.Instantiate(_prefab, position);
            view.Setup(config.NameId,config.Icon, config.Damage, config.Health, isUnlocked);
            return view;
        }
    }
}