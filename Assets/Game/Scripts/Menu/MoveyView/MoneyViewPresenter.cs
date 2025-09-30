using System;
using System.Globalization;
using Game.Scripts.Modules.Currency;
using Game.Scripts.Modules.SaveLoad;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.MoveyView
{
    public class MoneyViewPresenter : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _coinText;
        [SerializeField] 
        private TMP_Text _diamondText;

        private CurrencyCell _coinCurrency;
        private CurrencyCell _diamondCurrency;

        [Inject]
        public void Construct(CurrencyBank currencyBank)
        {
            _coinCurrency = currencyBank.GetCell(CurrencyType.COIN);
            _diamondCurrency = currencyBank.GetCell(CurrencyType.DIAMONDS);
        }

        private void OnEnable()
        {
            _coinCurrency.OnAmountChanged += OnMoneyChanged;
            _diamondCurrency.OnAmountChanged += OnMoneyChanged;
            OnMoneyChanged(0f);
        }

        private void OnDisable()
        {
            _coinCurrency.OnAmountChanged -= OnMoneyChanged;
            _diamondCurrency.OnAmountChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(float money)
        {
            _coinText.text = _coinCurrency.Amount.ToString(CultureInfo.CurrentCulture);
            _diamondText.text = _diamondCurrency.Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}