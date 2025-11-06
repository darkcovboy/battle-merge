using System;
using System.Globalization;
using Game.Scripts.Menu.UI.Roullete;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Battler.UI.Win
{
    public class WinScreenView : MonoBehaviour
    {
        public Action OnContinueButtonClicked;
        public Action OnRewardButtonClicked;

        [SerializeField] private GameObject[] _objectsToHide;
        [SerializeField] private GameObject _diamondsObject;
        [SerializeField] private TextMeshProUGUI _diamondsText;
        [SerializeField] private RewardRoulette _rewardRoulette;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _rewardButton;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueButtonClicked);
            _rewardButton.onClick.AddListener(RewardButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueButtonClicked);
            _rewardButton.onClick.RemoveListener(RewardButtonClicked);
        }
        
        public void SetValue(float income, float diamondsValue)
        {
            _rewardRoulette.SetValue(income);
            _diamondsText.text = $"+{diamondsValue.ToString(CultureInfo.InvariantCulture)}";
            _diamondsObject.SetActive(diamondsValue > 0);
        }

        public void Show()
        {
            foreach (var hideObject in _objectsToHide)
            {
                hideObject.SetActive(false);
            }
            gameObject.SetActive(true);
        }

        private void ContinueButtonClicked()
        {
            OnContinueButtonClicked?.Invoke();
        }

        private void RewardButtonClicked()
        {
            _rewardRoulette.OnButtonClicked();
            OnRewardButtonClicked?.Invoke();
        }
    }
}