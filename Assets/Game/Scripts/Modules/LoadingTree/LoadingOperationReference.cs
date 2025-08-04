using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Modules.LoadingTree
{
    public class LoadingOperationReference : LoadingOperation
    {
        public override string Name
        {
            get
            {
                if (this.taskAsset != null)
                {
                    return this.taskAsset.name;
                }

                return "Undefined";
            }
        }

        [SerializeField]
        private LoadingTaskAsset taskAsset;

        private LoadingOperation operation;

        public override float Progress => operation?.Progress ?? 0;

        public override async UniTask<Result> Run(LoadingBundle bundle)
        {
            LoadingTaskAsset clone = ScriptableObject.Instantiate(this.taskAsset);
         
            operation = clone.Operation;
            return await operation.Run(bundle);
        }
    }
}