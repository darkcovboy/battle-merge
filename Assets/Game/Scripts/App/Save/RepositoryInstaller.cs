using Game.Scripts.Modules.SaveLoad.Repository;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Save
{
    [CreateAssetMenu(
        fileName = "RepositoryInstaller",
        menuName = "Zenject/App/New RepositoryInstaller"
    )]
    public class RepositoryInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameRepository>().AsSingle();
        }
    }
}