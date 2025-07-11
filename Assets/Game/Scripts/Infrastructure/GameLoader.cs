using System.Collections;
using Game.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using CoroutineRunner = Game.Scripts.Useful.CoroutineRunner;

namespace Game.Scripts.Infrastructure
{
    public class GameLoader : IGameLoader
    {
        private const string SceneName = "Main";
        private readonly LoadingWindow _loadingWindow;

        public GameLoader(LoadingWindow loadingWindow)
        {
            _loadingWindow = loadingWindow;
        }

        public void LoadInitialScene()
        {
            _loadingWindow.Show("Loading...");
            CoroutineRunner.Instance.StartCoroutine(LoadMainMenuScene());
        }

        private IEnumerator LoadMainMenuScene()
        {
            yield return new WaitForSeconds(1f); // имитация загрузки

            var async = SceneManager.LoadSceneAsync(SceneName);
            while (!async.isDone)
            {
                _loadingWindow.SetProgress(async.progress);
                yield return null;
            }
        }

    }

    public interface IGameLoader
    {
        void LoadInitialScene();
    }
}