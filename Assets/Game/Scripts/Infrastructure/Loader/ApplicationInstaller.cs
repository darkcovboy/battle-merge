using Game.Scripts.Modules.LoadingTree;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Loader
{
    [CreateAssetMenu(
        fileName = "ApplicationInstaller",
        menuName = "Zenject/New ApplicationInstaller"
    )]
    public class ApplicationInstaller : ScriptableObjectInstaller
    {
        [SerializeReference]
        private ILoadingOperation _loadingOperation;
    
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameLauncher>()
                .AsSingle()
                .WithArguments(_loadingOperation)
                .OnInstantiated<GameLauncher>((_, launcher) => launcher.Launch())
                .NonLazy();
        }
    }
}