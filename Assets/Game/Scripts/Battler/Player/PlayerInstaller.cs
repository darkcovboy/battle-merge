using Game.Scripts.App.Characters.Data;
using Game.Scripts.App.Save.Serializers;
using Game.Scripts.Battler.Input;
using Game.Scripts.Battler.Monsters;
using Game.Scripts.Modules.SaveLoad;
using Game.Scripts.Modules.SaveLoad.Serializers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CharacterConfigCatalog _characterConfigCatalog;
        public override void InstallBindings()
        {
            if (Application.isMobilePlatform)
            {
                Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
            }

            Container.Bind<CharacterConfigCatalog>().FromInstance(_characterConfigCatalog).AsSingle();

            Container.Bind<MonsterFactory>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameSaveLoader>().AsSingle();

            Container.Bind<IGameSerializer[]>().FromMethod((context) =>
            {
                return new IGameSerializer[]
                {
                    context.Container.Instantiate<FieldSerializer>()
                };
            }).AsSingle();
        }
    }
}