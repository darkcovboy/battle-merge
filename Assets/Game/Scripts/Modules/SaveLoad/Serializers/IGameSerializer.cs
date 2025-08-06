using System.Collections.Generic;

namespace Game.Scripts.Modules.SaveLoad.Serializers
{
    public interface IGameSerializer
    {
        void Serialize(IDictionary<string, string> saveState);
        void Deserialize(IDictionary<string, string> loadState);

        void SetupDefault();
    }
}