using Game.Scripts.App.GameScenes;
using Game.Scripts.App.GameScenes.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure.Loader
{
    [CreateAssetMenu(
        fileName = "SceneLoaderInstaller",
        menuName = "Zenject/New SceneLoaderInstaller"
    )]
    public class SceneLoaderInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameSceneConfig _gameSceneConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();

            Container.Bind<GameSceneManager>().AsSingle().WithArguments(_gameSceneConfig).NonLazy();
        }
    }
}