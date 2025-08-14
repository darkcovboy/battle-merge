using Cysharp.Threading.Tasks;
using Game.Scripts.Modules.LoadingTree;
using Game.Scripts.Modules.SaveLoad;
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
                container = sceneContext.Container;
                GameSaveLoader gameSaveLoader = container.TryResolve<GameSaveLoader>();
                
                if (gameSaveLoader != null)
                {
                    gameSaveLoader.SetGameData(null);
                    gameSaveLoader.Load();
                }
            }
            
            return UniTask.FromResult(Result.Success());
        }
    }
}