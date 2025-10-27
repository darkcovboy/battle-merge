using System;
using System.Collections.Generic;
using Game.Scripts.Modules.Currency;
using Game.Scripts.Modules.SaveLoad;
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
        
        private List<int> _currentMultipliers = new List<int>();
        private int _multiplier;
        
        private CurrencyBank _bank;
        private GameSaveLoader _gameSaveLoader;

        [Inject]
        private void Construct(CurrencyBank currencyBank, GameSaveLoader gameSaveLoader)
        {
            _bank = currencyBank;
            _gameSaveLoader = gameSaveLoader;
        }

        private void OnEnable()
        {
            StartRoulette();
        }
        
        private void StartRoulette()
        {
            _currentMultipliers.Clear();
            _currentMultipliers.AddRange(_multipliers);
            
            _animator.enabled = true;
            _multiplierTexts[0].SetText(_currentMultipliers[0] + "X");
            _multiplierTexts[1].SetText(_currentMultipliers[1] + "X");
            _multiplierTexts[2].SetText(_currentMultipliers[2] + "X");
            _multiplierTexts[3].SetText(_currentMultipliers[3] + "X");
            _multiplierTexts[4].SetText(_currentMultipliers[2] + "X");
            _multiplierTexts[5].SetText(_currentMultipliers[1] + "X");
            _multiplierTexts[6].SetText(_currentMultipliers[0] + "X");
        }

        public void OnButtonClicked()
        {
            OnShowReward();
            //Добавить ревард
            _animator.enabled = false;
        }

        private void OnShowReward()
        {
            //добавляем деньгу
            //_gameSaveLoader.Save();
        }
        
        public void OnX5()
        {
            _multiplier = _currentMultipliers[0];
            UpdateView();
        }

        public void OnX10()
        {
            _multiplier = _currentMultipliers[1];
            UpdateView();
        }

        public void OnX15()
        {
            _multiplier = _currentMultipliers[2];
            UpdateView();
        }

        public void OnX20()
        {
            _multiplier = _currentMultipliers[3];
            UpdateView();
        }

        private void UpdateView()
        {
            //Добавить расчет инкома
            _multiplierText.text = $"+{(_multiplier/* * _gameRewardManager.CalculatedIncome.Value*/).ToString("##.#")}";
        }
    }
}