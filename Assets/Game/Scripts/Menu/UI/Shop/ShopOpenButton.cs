using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI.Shop
{
    public class ShopOpenButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private ShopPresenter _shopPresenter;
        
        [Inject]
        private void Construct(ShopPresenter shopPresenter)
        {
            _shopPresenter = shopPresenter;
            _button.onClick.AddListener(_shopPresenter.OpenShop);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}