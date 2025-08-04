using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Game.Scripts.Modules.LoadingTree
{
    public abstract class LoadingOperation : ILoadingOperation
    {
        public virtual string Name => this.GetType().Name;

#if ODIN_INSPECTOR
        [ProgressBar(0, 1), ReadOnly, HideInEditorMode]
#endif
        public virtual float Progress => 0;

        public abstract UniTask<Result> Run(LoadingBundle bundle);

        protected UniTask<Result> CompleteWithSuccess()
        {
            return UniTask.FromResult(Result.Success());
        }

        public UniTask<Result> CompleteWithFail(string error)
        {
            return UniTask.FromResult(Result.Fail(error));
        }

        public readonly struct Result
        {
            public readonly bool success;
            public readonly string error;

            private Result(bool success, string error)
            {
                this.success = success;
                this.error = error;
            }

            public override string ToString()
            {
                return $"{nameof(success)}: {success}, {nameof(error)}: {error}";
            }

            public static Result Success()
            {
                return new Result(true, null);
            }

            public static Result Fail(string error = null)
            {
                return new Result(false, error);
            }
        }
    }
}