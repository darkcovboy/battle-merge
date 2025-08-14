using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Modules.SaveLoad.Repository
{
    public class GameRepository : IGameRepository
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

        public void SetRepository(Dictionary<string, string> repository)
        {
            _repository = repository;
        }

        public Dictionary<string, string> GetState()
        {
            return _repository;
        }

        public void SetState(Dictionary<string, string> gameState)
        {
            foreach (var kvp in gameState)
            {
                _repository[kvp.Key] = kvp.Value;
            }

            SaveState();
        }

        public bool TryGetData<T>(out T value)
        {
            value = default;
            if (_repository.TryGetValue(typeof(T).Name, out string json))
            {
                try
                {
                    value = JsonConvert.DeserializeObject<T>(json);
                    return true;
                }
                catch
                {
                    Debug.LogError($"Failed to deserialize value for key '{typeof(T).Name}'");
                }
            }

            return false;
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
            Debug.Log(json);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);
            PlayerPrefs.Save();
            Debug.Log("Save state");
        }
    }
}