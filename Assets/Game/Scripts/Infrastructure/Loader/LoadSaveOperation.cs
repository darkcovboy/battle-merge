using Cysharp.Threading.Tasks;
using Game.Scripts.Modules.LoadingTree;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Loader
{
    public class LoadSaveOperation : LoadingOperation
    {
        public override UniTask<Result> Run(LoadingBundle bundle)
        {
            SceneContext sceneContext = GameObject.FindObjectOfType<SceneContext>();
            DiContainer container = null;

            if (sceneContext != null)
            {
                
            }
            
            return UniTask.FromResult(Result.Success());
        }
    }
}