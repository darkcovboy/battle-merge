using Cysharp.Threading.Tasks;

namespace Game.Scripts.Modules.LoadingTree
{
    public interface ILoadingOperation
    {
        string Name { get; }
        
        float Progress { get; }
        
        UniTask<LoadingOperation.Result> Run(LoadingBundle bundle);
    }
}