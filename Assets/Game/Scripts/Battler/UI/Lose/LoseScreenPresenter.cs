using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.Ads;
using Game.Scripts.App.GameScenes;
using Game.Scripts.App.GameScenes.GameReward;
using Game.Scripts.Battler.StateGame;
using Game.Scripts.Infrastructure.Loader;
using Game.Scripts.Modules.Currency;
using UniRx;

namespace Game.Scripts.Battler.UI.Lose
{
    public class LoseScreenPresenter : IDisposable, ILoseHandler
    {
        private readonly LoseScreenView _view;
        private readonly MatchStateService _matchStateService;
        private readonly RewardManager _rewardManager;
        private readonly GameSceneManager _gameSceneManager;
        private readonly CurrencyBank _currencyBank;
        private readonly CompositeDisposable _disposables = new();


        public LoseScreenPresenter(LoseScreenView view, MatchStateService matchStateService, RewardManager rewardManager, GameSceneManager gameSceneManager, CurrencyBank currencyBank)
        {
            _view = view;
            _matchStateService = matchStateService;
            _rewardManager = rewardManager;
            _gameSceneManager = gameSceneManager;
            _currencyBank = currencyBank;
            _view.OnContinueButtonClicked += OnContinueButtonClicked;
            _view.OnRewardButtonClicked += OnRewardButtonClicked;

            _matchStateService.Result
                .Where(result => result == GameResult.Lose)
                .Subscribe(_ => OnLose(LoseReason.PlayerDead))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _view.OnContinueButtonClicked -= OnContinueButtonClicked;
            _view.OnRewardButtonClicked -= OnRewardButtonClicked;
        }

        public void OnLose(LoseReason reason)
        {
            _view.SetValue(_rewardManager.CalculateLevelReward(_matchStateService.EnemiesKilled.Value).Coins);
            _view.Show();
        }

        private async void OnRewardButtonClicked()
        {
            await UniTask.Delay(2000);
            _gameSceneManager.LoseLevel();
        }

        private void OnContinueButtonClicked()
        {
            _currencyBank.GetCell(CurrencyType.COIN)
                .Add(_rewardManager.CalculateLevelReward(_matchStateService.EnemiesKilled.Value).Coins);
            AdsManager.Instance.ShowInterstitialAd();
            _gameSceneManager.LoseLevel();
        }
    }
}