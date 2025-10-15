using System.Linq;
using Game.Scripts.App.Characters.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Monsters
{
    public class MonsterFactory : IFactory<Vector3, string, MonsterCharacter>
    {
        private readonly CharacterConfigCatalog _catalog;

        public MonsterFactory(CharacterConfigCatalog catalog)
        {
            _catalog = catalog;
        }

        public MonsterCharacter Create(Vector3 position, string nameId)
        {
            CharacterConfig config = _catalog.CharacterConfigs
                .FirstOrDefault(c => c.NameId == nameId);

            MonsterCharacter monsterCharacter = Object.Instantiate(config.MonsterCharacterPrefab, position, Quaternion.identity);

            monsterCharacter.Initialize(config);
            return monsterCharacter;
        }
    }
}