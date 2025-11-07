using Cysharp.Threading.Tasks;
using Game.Scripts.Menu.Field;
using Game.Scripts.Modules.LoadingTree;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class StartGameOperation : LoadingOperation
    {
        public async override UniTask<Result> Run(LoadingBundle bundle)
        {
            SceneContext sceneContext = GameObject.FindObjectOfType<SceneContext>();

            if (sceneContext != null)
            {
                var initiable = sceneContext.Container.TryResolve<IReady>();

                if (initiable != null)
                {
                    await UniTask.WaitUntil(() => initiable.IsReady());

                }
            }
            
            return Result.Success();
        }
    }
}