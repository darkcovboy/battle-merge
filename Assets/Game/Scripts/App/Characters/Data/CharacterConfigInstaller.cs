using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Characters.Data
{
    [CreateAssetMenu(
        fileName = "CharacterConfigInstaller",
        menuName = "Zenject/New CharacterConfigInstaller"
    )]
    public class CharacterConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CharacterConfigCatalog _characterConfigCatalog;

        public override void InstallBindings()
        {
            Container.Bind<CharacterConfigCatalog>().FromInstance(_characterConfigCatalog).AsSingle();
        }
    }
}