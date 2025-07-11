using Game.Scripts.UI;
using Game.Scripts.Useful;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Installers
{
    public class InitInstaller : MonoInstaller
    {
        [SerializeField] private LoadingWindow _loadingWindowPrefab;
        [SerializeField] private DontDestroyOnLoadScript _singletonsPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IGameLoader>().To<GameLoader>().AsSingle();
            Container.Bind<DontDestroyOnLoadScript>().FromComponentInNewPrefab(_singletonsPrefab).AsSingle().NonLazy();
            Container.Bind<LoadingWindow>().FromComponentInNewPrefab(_loadingWindowPrefab).AsSingle().NonLazy();
        }

    }
}