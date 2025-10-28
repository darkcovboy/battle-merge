using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.Ads;
using Game.Scripts.App.GameScenes;
using Game.Scripts.App.GameScenes.GameReward;
using Game.Scripts.Battler.StateGame;
using Game.Scripts.Infrastructure.Loader;
using UniRx;

namespace Game.Scripts.Battler.UI.Win
{
    public class WinScreenPresenter : IWinHandler, IDisposable
    {
        private readonly WinScreenView _view;
        private readonly MatchStateService _matchStateService;
        private readonly GameSceneManager _gameSceneManager;
        private readonly RewardManager _rewardManager;
        private readonly CompositeDisposable _disposables = new();


        public WinScreenPresenter(WinScreenView view, MatchStateService matchStateService, GameSceneManager gameSceneManager, RewardManager rewardManager)
        {
            _view = view;
            _matchStateService = matchStateService;
            _gameSceneManager = gameSceneManager;
            _rewardManager = rewardManager;
            _view.OnContinueButtonClicked += OnContinueButtonClicked;
            _view.OnRewardButtonClicked += OnRewardButtonClicked;

            _matchStateService.Result
                .Where(result => result == GameResult.Win)
                .Subscribe(_ => OnWin(WinReason.AllEnemiesDead))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _view.OnContinueButtonClicked -= OnContinueButtonClicked;
            _view.OnRewardButtonClicked -= OnRewardButtonClicked;
        }

        public void OnWin(WinReason reason)
        {
            RewardData rewardData = _rewardManager.CalculateLevelReward(_matchStateService.EnemiesKilled.Value);
            _view.SetValue(rewardData.Coins,rewardData.Gems);
            _view.Show();
        }

        private async void OnRewardButtonClicked()
        {
            await UniTask.Delay(2000);
            _gameSceneManager.CompleteLevel();
        }

        private void OnContinueButtonClicked()
        {
            AdsManager.Instance.ShowInterstitialAd();
            _gameSceneManager.CompleteLevel();
        }
    }
}