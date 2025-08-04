using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Modules.LoadingTree
{
    public class LoadingOperationSequence : LoadingOperationComposite, ISerializationCallbackReceiver
    {
        [ShowInInspector, ReadOnly, HideInEditorMode]
        private int _fullWeight;
        private int _accWeight;

        private TaskInfo _currentTask;

        public override float Progress
        {
            get
            {
                float taskWeight = _currentTask?.CurrentWeight ?? 0;
                return (_accWeight + taskWeight) / _fullWeight;
            }
        }

        [Button, PropertyOrder(-100), HideInEditorMode]
        public override async UniTask<Result> Run(LoadingBundle bundle)
        {
            _accWeight = 0;

            for (int i = 0, count = this.taskList.Length; i < count; i++)
            {
                TaskInfo taskInfo = this.taskList[i];
                if (taskInfo.off)
                {
                    continue;
                }

                _currentTask = taskInfo;

#if UNITY_EDITOR
                string name = _currentTask.operation.GetType().Name;
                if (this.showDebug)
                {
                    Debug.Log($"<color=green>Start Operation: {name}</color>");
                }
#endif

                Result result;

                try
                {
                    result = await _currentTask.operation.Run(bundle);
                }
                catch (Exception e)
                {
                    result = Result.Fail(e.Message + "\n" + e.StackTrace);
                }

#if UNITY_EDITOR
                if (this.showDebug)
                {
                    if (result.success)
                    {
                        Debug.Log(
                            $"<color=green>End Operation: {name} {result}</color>");
                    }
                    else
                    {
                        Debug.LogError(
                            $"<color=red>End Operation: {name} {result}</color>");
                    }
                }
#endif

                if (!result.success)
                {
                    return Result.Fail(result.error);
                }

                _accWeight += _currentTask.weight;
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