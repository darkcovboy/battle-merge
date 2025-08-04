using UnityEngine;

namespace Game.Scripts.Modules.LoadingTree
{
    [CreateAssetMenu(
        fileName = "LoadingOperationAsset",
        menuName = "Loading/New LoadingOperationAsset"
    )]
    public class LoadingTaskAsset : ScriptableObject
    {
        [field: SerializeReference]
        public LoadingOperation Operation { get; private set; }
    }
}