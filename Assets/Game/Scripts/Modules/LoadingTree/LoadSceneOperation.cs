using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Modules.LoadingTree
{
    [Serializable]
    public class LoadSceneOperation : LoadingOperation
    {
        [SerializeField]
        private string sceneName;

        [SerializeField]
        private LoadSceneMode sceneMode;
        
        private AsyncOperation _operation;

        public override float Progress => _operation?.progress ?? 0;

        public override async UniTask<Result> Run(LoadingBundle bundle)
        {
            _operation = SceneManager.LoadSceneAsync(this.sceneName, this.sceneMode);
            await _operation;
            return Result.Success();
        }
    }
}