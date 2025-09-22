using System;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Menu.Field;
using Game.Scripts.Modules.Currency;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.BuyButtons
{
    public class BuyButtonCharacterPresenter : IInitializable, IDisposable
    {
        private readonly BuyButtonCharacterView[] _views;
        private readonly IFieldPresenter _fieldPresenter;
        private readonly CurrencyCell _moneyCell;
        
        private const int FighterPrice = 100;
        private const int ShooterPrice = 100;


        public BuyButtonCharacterPresenter(BuyButtonCharacterView[] views, IFieldPresenter fieldPresenter, CurrencyBank currencyBank)
        {
            _views = views;
            _fieldPresenter = fieldPresenter;
            _moneyCell = currencyBank.GetCell(CurrencyType.COIN);
        }
        
        public void Initialize()
        {
            foreach (var view in _views) view.OnClick += OnButtonClicked;

            _moneyCell.OnStateChanged += RefreshButtons;
            RefreshButtons();
        }

        public void Dispose()
        {
            foreach (var view in _views)
            {
                view.OnClick -= OnButtonClicked;
            }
            
            _moneyCell.OnStateChanged -= RefreshButtons;
        }
        
        private void RefreshButtons()
        {
            foreach (var view in _views)
            {
                int price = GetPrice(view.CharacterWarriorType);
                bool canAfford = _moneyCell.Exists(price);
                
                view.SetInteractable(canAfford);
            }
        }


        private void OnButtonClicked(CharacterWarriorType characterWarriorType)
        {
            int price = GetPrice(characterWarriorType);

            if (_moneyCell.Exists(price))
            {
                if (_moneyCell.Spend(price))
                {
                    switch (characterWarriorType)
                    {
                        case CharacterWarriorType.Fighter:
                            _fieldPresenter.AddCharacter("fighter_1");
                            break;
                        case CharacterWarriorType.Shooter:
                            _fieldPresenter.AddCharacter("shooter_1");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
        
        private int GetPrice(CharacterWarriorType type)
        {
            return type switch
            {
                CharacterWarriorType.Fighter => FighterPrice,
                CharacterWarriorType.Shooter => ShooterPrice,
                _ => 100
            };
        }

    }
}