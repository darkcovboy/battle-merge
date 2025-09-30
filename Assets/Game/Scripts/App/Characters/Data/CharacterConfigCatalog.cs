using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.App.Characters.Data
{
    [CreateAssetMenu(fileName = "CharacterConfigCatalog", menuName = "Configs/CharacterConfigCatalog")]
    public class CharacterConfigCatalog : ScriptableObject
    {
        private const int Price = 100;
        
        [SerializeField] private List<CharacterConfig> _characterConfigs;
        
        public List<CharacterConfig> CharacterConfigs => _characterConfigs;
        
        private Dictionary<CharacterWarriorType, List<CharacterConfig>> _lines;

        public void Init()
        {
            _lines = _characterConfigs
                .GroupBy(c => c.WarriorType)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(c =>
                    {
                        var parts = c.NameId.Split('_');
                        return parts.Length > 1 && int.TryParse(parts[1], out int lvl) ? lvl : 0;
                    }).ToList()
                );
        }

        public CharacterConfig GetNext(CharacterConfig current)
        {
            var line = _lines[current.WarriorType];
            int index = line.IndexOf(current);
            if (index >= 0 && index < line.Count - 1)
                return line[index + 1];

            return null;
        }

        public int GetPrice(CharacterConfig current)
        {
            var line = _lines[current.WarriorType];
            int index = line.IndexOf(current);
        
            if (index >= 0)
            {
                return Price * (int)Mathf.Pow(2, index);
            }
        
            return Price;
        }
    }
}