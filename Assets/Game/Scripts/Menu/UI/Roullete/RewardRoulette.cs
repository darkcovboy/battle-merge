using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.App.Ads;
using Game.Scripts.Modules.Currency;
using Game.Scripts.Modules.SaveLoad;
using Game.Scripts.Useful.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI.Roullete
{
    public class RewardRoulette : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private TMP_Text _multiplierText;

        [SerializeField]
        private int[] _multipliers;

        [SerializeField]
        private TMP_Text[] _multiplierTexts;

        [SerializeField]
        private Sprite[] _multiplierSprites;
        
        private int _multiplier;
        
        private CurrencyBank _bank;
        private GameSaveLoader _gameSaveLoader;
        private Coroutine _coroutine;

        private float _incomeValue;

        [Inject]
        private void Construct(CurrencyBank currencyBank, GameSaveLoader gameSaveLoader)
        {
            _bank = currencyBank;
            _gameSaveLoader = gameSaveLoader;
        }

        public void SetValue(float income)
        {
            _incomeValue = income;
        }

        private void OnEnable()
        {
            StartRoulette();
        }
        
        private void StartRoulette()
        {
            _animator.enabled = true;
        }

        public void OnButtonClicked()
        {
            AdsManager.Instance.ShowRewardedAd(OnShowReward);
            _animator.enabled = false;
        }

        private void OnShowReward()
        {
            var income = _incomeValue * _multiplier;
            _bank.GetCell(CurrencyType.COIN).Add(income);
            _gameSaveLoader.Save();
        }
        
        public void OnX5()
        {
            _multiplier = _multipliers[0];
            UpdateView();
        }

        public void OnX10()
        {
            _multiplier = _multipliers[1];
            UpdateView();
        }

        public void OnX15()
        {
            _multiplier = _multipliers[2];
            UpdateView();
        }

        public void OnX20()
        {
            _multiplier = _multipliers[3];
            UpdateView();
        }

        private void UpdateView()
        {
            _multiplierText.text = $"+{(_multiplier * _incomeValue).ToShortString()}";
        }
    }
}