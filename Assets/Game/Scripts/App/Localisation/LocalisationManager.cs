using System.Collections.Generic;
using GamePush;

namespace Game.Scripts.App.Localisation
{
    public class LocalisationManager
    {
        private List<LocalisationDataSO> _localisationDatas;
        private readonly Language _language;
        private Dictionary<string, LocalisationData> _dictionary = new();

        public LocalisationManager(Language language, List<LocalisationDataSO> localisationDatas)
        {
            _language = language;
            _localisationDatas = localisationDatas;
        }

        public void CreateDictionary()
        {
            foreach (var itemLocalisationData in _localisationDatas)
            {
                foreach (var itemInData in itemLocalisationData.LocalisationList)
                {
                    _dictionary.TryAdd(itemInData.Key.ToUpper(), itemInData);
                }
            }
        }

        public string GetText(string key, params object[] tokens)
        {
            var text = _language switch
            {
                Language.Russian => _dictionary[key.ToUpper()].RussianText,
                _ => _dictionary[key.ToUpper()].EnglishText
            };
            
            if (string.IsNullOrEmpty(text)) text = _dictionary[key.ToUpper()].EnglishText;
            
            for (int i = 0; i < tokens.Length; i++)
            {
                text = text.Replace($"{{T{i}}}", tokens[i].ToString());
            }

            return text;
        }
    }
}