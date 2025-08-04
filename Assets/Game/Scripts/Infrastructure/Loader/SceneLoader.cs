using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Infrastructure.Loader
{
    public class SceneLoader
    {
        private readonly LoadingScreen.LoadingScreen _loadingScreen;

        public SceneLoader(LoadingScreen.LoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
        }

        public async UniTaskVoid LoadSceneAsync(string sceneName)
        {
            _loadingScreen.Show();

            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }
            
            _loadingScreen.Hide();
        }
    }
}