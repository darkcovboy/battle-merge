using System;
using Game.Scripts.Menu.UI.Roullete;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Battler.UI.Lose
{
    public class LoseScreenView : MonoBehaviour
    {
        public Action OnContinueButtonClicked;
        public Action OnRewardButtonClicked;
        
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

        public void Show()
        {
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