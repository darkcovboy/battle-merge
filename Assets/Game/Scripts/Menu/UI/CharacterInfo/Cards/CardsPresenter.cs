using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Menu.CharacterUnlock;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.UI.CharacterInfo.Cards
{
    public class CardsPresenter : IInitializable, IDisposable
    {
        private readonly CardsFactory _cardsFactory;
        private readonly CardsView _cardsView;
        private readonly ICharacterUnlockService _characterUnlockService;
        private readonly CharacterConfigCatalog _configCatalog;
        
        private List<CardCellView> _cardViews = new List<CardCellView>();

        public CardsPresenter(CardsView cardsView, CardsFactory cardsFactory,
            ICharacterUnlockService characterUnlockService, CharacterConfigCatalog configCatalog)
        {
            _cardsView = cardsView;
            _cardsFactory = cardsFactory;
            _characterUnlockService = characterUnlockService;
            _configCatalog = configCatalog;
            _characterUnlockService.CharacterUnlocked += UnlockCharacter;
        }

        public void Initialize()
        {
            SetupCards().Forget();
        }

        public void Dispose()
        {
            _characterUnlockService.CharacterUnlocked -= UnlockCharacter;
        }

        private void UnlockCharacter(CharacterConfig characterConfig)
        {
            CardCellView cardCellView = _cardViews.Find(x => x.NameId == characterConfig.NameId);

            if (cardCellView != null)
                cardCellView.Unlock();
        }

        private async UniTaskVoid SetupCards()
        {
            SpawnCards(CharacterWarriorType.Fighter);
            await UniTask.WaitForEndOfFrame();
            SpawnCards(CharacterWarriorType.Shooter);
        }

        private void SpawnCards(CharacterWarriorType characterWarriorType)
        {
            foreach (var config in _configCatalog.Lines[characterWarriorType])
            {
                Transform container = GetCurrentContainer(characterWarriorType);
                bool unlocked = _characterUnlockService.CharactersUnlocked.Contains(config.NameId);
                
                _cardViews.Add(_cardsFactory.Create(container, unlocked, config));
            }

            Transform GetCurrentContainer(CharacterWarriorType type)
            {
                switch (type)
                {
                    case CharacterWarriorType.Fighter:
                        return _cardsView.CardsFightersContainer;
                    case CharacterWarriorType.Shooter:
                        return _cardsView.CardsShootersContainer;
                    default:
                        return _cardsView.CardsFightersContainer;
                }
            }
        }
    }
}