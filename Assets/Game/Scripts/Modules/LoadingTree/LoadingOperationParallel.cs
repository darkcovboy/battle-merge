using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Modules.LoadingTree
{
    public class LoadingOperationParallel : LoadingOperationComposite, ISerializationCallbackReceiver
    {
        [ShowInInspector, ReadOnly, HideInEditorMode]
        private int _fullWeight;

        public override float Progress
        {
            get
            {
                float currentWeight = 0;
                for (int i = 0, count = this.taskList.Length; i < count; i++)
                {
                    TaskInfo task = this.taskList[i];
                    currentWeight += task.CurrentWeight;
                }

                return currentWeight / _fullWeight;
            }
        }

        [Button, PropertyOrder(-100), HideInEditorMode]
        public override async UniTask<Result> Run(LoadingBundle bundle)
        {
            int count = this.taskList.Length;
            var tasks = new List<UniTask<Result>>(count);

            for (int i = 0; i < count; i++)
            {
                TaskInfo taskInfo = this.taskList[i];
                if (taskInfo.off)
                {
                    continue;
                }
                
                tasks[i] = taskInfo.operation.Run(bundle);
            }

            Result[] results = await UniTask.WhenAll(tasks);
            for (int i = 0; i < count; i++)
            {
                Result result = results[i];
                if (!result.success)
                {
                    return Result.Fail(result.error);
                }
            }

            return Result.Success();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _fullWeight = this.taskList
                .Where(it => !it.off)
                .Sum(it => it.weight);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}