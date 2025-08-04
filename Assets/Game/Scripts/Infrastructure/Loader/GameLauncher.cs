using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Scripts.Modules.LoadingTree;

namespace Game.Scripts.Infrastructure.Loader
{
    public class GameLauncher
    {
        private ILoadingOperation _operation;
        private readonly LoadingScreen.LoadingScreen _loadingScreen;
        private bool _isLoading;

        public GameLauncher(ILoadingOperation operation, LoadingScreen.LoadingScreen loadingScreen)
        {
            _operation = operation;
            _loadingScreen = loadingScreen;
        }

        public void SetLoadingOperation(ILoadingOperation operation)
        {
            _operation = operation;
        }

        public void Launch()
        {
            this.LoadInternal().Forget();
            this.UpdateProgress().Forget();
        }

        public async UniTask LaunchWithDelay(float delay)
        {
            await UniTask.WaitForSeconds(delay);
            this.LoadInternal().Forget();
            this.UpdateProgress().Forget();
        }

        private async UniTaskVoid LoadInternal()
        {
            _isLoading = true;
            _loadingScreen.Show();     

            var bundle = new LoadingBundle(
                new KeyValuePair<string, object>(LoadingBundleKeys.Level, 1)
            );
            await _operation.Run(bundle);

            _loadingScreen.Hide();
            _isLoading = false;
        }

        private async UniTaskVoid UpdateProgress()
        {
            while (_isLoading)
            {
                float progress = _operation.Progress;
                //_loadingScreen.SetProgress(progress);
                await UniTask.Yield();
            }
        }
    }
}