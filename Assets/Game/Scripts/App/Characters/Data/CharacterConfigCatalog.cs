using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.App.Characters.Data
{
    [CreateAssetMenu(fileName = "CharacterConfigCatalog", menuName = "Configs/CharacterConfigCatalog")]
    public class CharacterConfigCatalog : ScriptableObject
    {
        [SerializeField] private List<CharacterConfig> _characterConfigs;
        
        public List<CharacterConfig> CharacterConfigs => _characterConfigs;
    }
}