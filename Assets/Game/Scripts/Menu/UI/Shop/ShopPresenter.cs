using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Menu.UI.Shop.Data;
using Game.Scripts.Menu.UI.Shop.ModelsDisplay;
using Game.Scripts.Modules.Currency;
using Game.Scripts.Modules.SaveLoad;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts.Menu.UI.Shop
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private readonly ShopView _view;
        private readonly HeroViewConfigHolder _configHolder;
        private readonly HeroCardView _prefab;
        private readonly SkinPlacement _skinPlacement;
        private readonly GameSaveLoader _gameSaveLoader;
        private readonly SkinService _skinService;
        private readonly CurrencyCell _currencyCell;
        private List<HeroCardView> _cards = new();
        
        private int _selectedSkinId;


        public ShopPresenter(ShopView view, 
            HeroViewConfigHolder configHolder,
            HeroCardView prefab,
            SkinPlacement skinPlacement,
            GameSaveLoader gameSaveLoader,
            SkinService skinService,
            CurrencyBank currencyBank)
        {
            _view = view;
            _configHolder = configHolder;
            _prefab = prefab;
            _skinPlacement = skinPlacement;
            _gameSaveLoader = gameSaveLoader;
            _skinService = skinService;
            _currencyCell = currencyBank.GetCell(CurrencyType.DIAMONDS);

            _view.OnBuyClicked += OnBuyClicked;
            _view.OnSelectClicked += OnSelectClicked;
            _view.OnBackClicked += OnBackClicked;
            _currencyCell.OnAmountChanged += OnCurrencyChanged;
        }

        public void Initialize()
        {
            _view.HideAllActionButtons();
            SpawnCards();
            ShowCurrentSkin();
        }

        public void Dispose()
        {
            _currencyCell.OnAmountChanged -= OnCurrencyChanged;
            _view.OnBuyClicked -= OnBuyClicked;
            _view.OnSelectClicked -= OnSelectClicked;
            _view.OnBackClicked -= OnBackClicked;
            
            foreach (var card in _cards)
                card.OnClicked -= OnCardSelected;
        }

        private void SpawnCards()
        {
            foreach (var config in _configHolder.Skins)
            {
                var card = Object.Instantiate(_prefab, _view.CardsParent);
                card.Setup(config.Id, config.Icon, config.Price);
                card.SetUnlocked(_skinService.UnlockedSkins.Contains(config.Id));
                card.OnClicked += OnCardSelected;
                _cards.Add(card);
            }
        }
        
        private void OnCurrencyChanged(float _)
        {
            if (_selectedSkinId == 0)
                return;

            var config = _configHolder.GetById(_selectedSkinId);
            if (config != null)
                _view.SetBuyButtonState(_currencyCell.Exists(config.Price));
        }
        
        private void ShowCurrentSkin()
        {
            var config = _configHolder.GetById(_skinService.CurrentSkin);
            if (config != null)
                _skinPlacement.InstantiateModel(config.ModelPrefab.gameObject);
        }

        private void OnCardSelected(int id)
        {
            _selectedSkinId = id;
            var config = _configHolder.GetById(id);
            if (config == null)
                return;
            

            _skinPlacement.InstantiateModel(config.ModelPrefab.gameObject);

            bool unlocked = _skinService.UnlockedSkins.Contains(id);
            bool isCurrent = _skinService.CurrentSkin == id;

            if (!unlocked)
                _view.ShowBuyButton();
            else if (!isCurrent)
                _view.ShowSelectButton();
            else
                _view.HideAllActionButtons();
        }

        private void OnBuyClicked()
        {
            var config = _configHolder.GetById(_selectedSkinId);
            if (config == null)
                return;


            if (!_currencyCell.Exists(config.Price))
            {
                Debug.Log("[SHOP] Not enough currency!");
                // можно тут вызвать popup или disable кнопку
                return;
            }

            if (_currencyCell.Spend(config.Price))
            {
                _skinService.UnlockedSkins.Add(_selectedSkinId);
                UpdateCardVisuals();
                _view.ShowSelectButton();
                Save();
            }
        }
        
        private void UpdateCardVisuals()
        {
            foreach (var card in _cards)
                card.SetUnlocked(_skinService.UnlockedSkins.Contains(card.Id));
        }

        private void OnSelectClicked()
        {
            var config = _configHolder.GetById(_selectedSkinId);
            if (config == null)
                return;

            _skinService.CurrentSkin = config.Id;
            _view.HideAllActionButtons();
            Save();
        }

        public void OpenShop()
        {
            _view.Show();
        }

        private void OnBackClicked()
        {
            _view.Hide();
        }
        
        private void Save()
        {
            _gameSaveLoader.Save();
        }
    }
}