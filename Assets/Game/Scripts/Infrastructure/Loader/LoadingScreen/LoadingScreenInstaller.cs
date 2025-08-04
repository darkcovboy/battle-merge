using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Loader.LoadingScreen
{
    [CreateAssetMenu(
        fileName = "LoadingScreenInstaller",
        menuName = "Zenject/New LoadingScreenInstaller"
    )]
    public class LoadingScreenInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private LoadingScreen _screenPrefab;
        
        public override void InstallBindings()
        {
            this.Container
                .Bind<LoadingScreen>()
                .FromComponentInNewPrefab(_screenPrefab)
                .AsSingle()
                .OnInstantiated<LoadingScreen>((_, it) => it.Hide())
                .NonLazy();
        }
    }
}