using Game.Scripts.App.Ads;
using Game.Scripts.Modules.Currency;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI.Shop
{
    public class ShopRewardButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _rewardAmount = 20;

        private CurrencyCell _diamondsCell;

        [Inject]
        public void Construct(CurrencyBank currencyBank)
        {
            _diamondsCell = currencyBank.GetCell(CurrencyType.DIAMONDS);
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnRewardButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnRewardButtonClicked);
        }

        private void OnRewardButtonClicked()
        {
            _button.interactable = false;

            AdsManager.Instance.ShowRewardedAd(() =>
            {
                _diamondsCell.Add(_rewardAmount);
                gameObject.SetActive(false);
            });
        }
    }
}