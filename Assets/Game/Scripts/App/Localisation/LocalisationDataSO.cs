
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.App.Localisation
{
    [CreateAssetMenu(fileName = "Localisation", menuName = "LocalisationSO", order = 0)]
    public class LocalisationDataSO : ScriptableObject
    {
        public List<LocalisationData> LocalisationList;
        
        private string GetFilePath()
        {
            return "Assets/Resources/Localisation/localisation.json";
        }

        [Button]
        public void ClearFile()
        {
            File.WriteAllText(GetFilePath(),string.Empty);
        }

        [Button]
        public void SaveToJson()
        {
            string filePath = GetFilePath();

            string json = JsonConvert.SerializeObject(LocalisationList, Formatting.Indented);
            ClearFile();
            File.WriteAllText(filePath, json);

            Debug.Log($"Localization data saved to {filePath}");
        }

        [Button]
        public void LoadFromJson()
        {
            string filePath = GetFilePath();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<LocalisationData> loadedList = JsonConvert.DeserializeObject<List<LocalisationData>>(json);
            
                LocalisationList.Clear();
            
                foreach (var loadedItem in loadedList)
                {
                    LocalisationList.Add(loadedItem);
                }

                Debug.Log("Localization data loaded from file");
            }
            else
            {
                Debug.LogWarning("Localization file not found");
            }
        }
    }

    [Serializable]
    public struct LocalisationData
    {
        public string Key;
        public string RussianText;
        public string EnglishText;
    }
}