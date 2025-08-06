using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Modules.SaveLoad.Repository
{
    public class GameRepository
    {
        private const string GAME_STATE_KEY = "GameState";

        private Dictionary<string, string> _repository;

        public GameRepository()
        {
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                _repository = new Dictionary<string, string>();
                SaveState();
            }
            else
            {
                _repository = LoadState();
            }
        }
        
        public T GetData<T>()
        {
            if (_repository.TryGetValue(typeof(T).Name, out string json))
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch
                {
                    Debug.LogError($"Failed to deserialize value for key '{typeof(T).Name}'");
                }
            }

            return default;
        }

        private Dictionary<string, string> LoadState()
        {
            string encryptedJson = PlayerPrefs.GetString(GAME_STATE_KEY);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptedJson) ??
                   new Dictionary<string, string>();
        }
        
        private void SaveState()
        {
            string json = JsonConvert.SerializeObject(_repository);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);
            PlayerPrefs.Save();
        }
    }
}