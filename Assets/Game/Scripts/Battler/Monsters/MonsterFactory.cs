using System.Linq;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Battler.DamageViews;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Monsters
{
    public class MonsterFactory : IFactory<Vector3, string,bool, MonsterCharacter>
    {
        private readonly CharacterConfigCatalog _catalog;
        private readonly IDamagePopupService _damagePopupService;

        public MonsterFactory(CharacterConfigCatalog catalog, IDamagePopupService damagePopupService)
        {
            _catalog = catalog;
            _damagePopupService = damagePopupService;
        }

        public MonsterCharacter Create(Vector3 position, string nameId, bool isPlayer)
        {
            CharacterConfig config = _catalog.CharacterConfigs
                .FirstOrDefault(c => c.NameId == nameId);

            MonsterCharacter monsterCharacter = Object.Instantiate(config.MonsterCharacterPrefab, position, Quaternion.identity);

            monsterCharacter.Initialize(config,_damagePopupService, isPlayer);
            return monsterCharacter;
        }
    }
}