using System.Collections.Generic;

namespace Game.Scripts.Modules.LoadingTree
{
    public class LoadingBundle
    {
        private readonly Dictionary<string, object> collection = new();

        public LoadingBundle(KeyValuePair<string, object> keyValuePair)
        {
            this.collection[keyValuePair.Key] = keyValuePair.Value;
        }

        public void SetData(string key, object value)
        {
            this.collection[key] = value;
        }

        public void RemoveData(string key)
        {
            this.collection.Remove(key);
        }

        public bool TryGetData<T>(string key, out T value)
        {
            if (this.collection.TryGetValue(key, out var result))
            {
                value = (T) result;
                return true;
            }

            value = default;
            return false;
        }
        
        public T GetData<T>(string key)
        {
            return (T) this.collection[key];
        }
    }
}