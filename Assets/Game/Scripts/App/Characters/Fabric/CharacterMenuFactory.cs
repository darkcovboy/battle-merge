using System.Linq;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.App.Characters.Menu;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Characters.Fabric
{
    public class CharacterMenuFactory : IFactory<Vector3, string, IDraggableCharacter>
    {
        private readonly CharacterConfigCatalog _characterConfigCatalog;

        public CharacterMenuFactory(CharacterConfigCatalog characterConfigCatalog)
        {
            _characterConfigCatalog = characterConfigCatalog;
        }
        
        public IDraggableCharacter Create(Vector3 position, string name)
        {
            CharacterConfig characterConfig = _characterConfigCatalog.CharacterConfigs
                .FirstOrDefault(c => c.NameId == name);

            GameObject instance = Object.Instantiate(characterConfig.ItemReference, position, Quaternion.identity, null);

            if (!instance.TryGetComponent<IDraggableCharacter>(out var draggableCharacter))
            {
                Debug.LogError($"ItemReference for {name} has no IDraggableCharacter component!");
                return null;
            }

            draggableCharacter.Config = characterConfig;
            draggableCharacter.Appear();
            return draggableCharacter;
        }
    }
}