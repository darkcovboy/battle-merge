using Game.Scripts.App.Save.Serializers;
using Game.Scripts.Modules.SaveLoad;
using Game.Scripts.Modules.SaveLoad.Serializers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Save
{
    public class SaveLoadMenuInstaller : Installer<SaveLoadMenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSaveLoader>().AsSingle();

            Container.Bind<IGameSerializer[]>().FromMethod((context) =>
            {
                return new IGameSerializer[]
                {
                    context.Container.Instantiate<TutorialSerializer>()
                };
            }).AsSingle();
        }
    }
}