using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.Battler.StateGame;
using Game.Scripts.Infrastructure.Loader;
using UniRx;

namespace Game.Scripts.Battler.UI.Win
{
    public class WinScreenPresenter : IWinHandler, IDisposable
    {
        private readonly WinScreenView _view;
        private readonly MatchStateService _matchStateService;
        private readonly SceneLoader _sceneLoader;
        private readonly CompositeDisposable _disposables = new();


        public WinScreenPresenter(WinScreenView view, MatchStateService matchStateService, SceneLoader sceneLoader)
        {
            _view = view;
            _matchStateService = matchStateService;
            _sceneLoader = sceneLoader;
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

        public void OnWin(WinReason reason) => _view.Show();

        private async void OnRewardButtonClicked()
        {
            await UniTask.Delay(2000);
            _sceneLoader.LoadSceneAsync("Menu").Forget();
        }

        private void OnContinueButtonClicked()
        {
            //Показать интер
            _sceneLoader.LoadSceneAsync("Menu").Forget();
        }
    }
}