using System.Collections.Generic;

namespace Game.Scripts.Modules.SaveLoad.Repository
{
    public interface IGameRepository
    {
        Dictionary<string, string> GetState();
        void SetState(Dictionary<string, string> gameState);
        bool TryGetData<T>(out T value);
        T GetData<T>();
        void SetRepository(Dictionary<string, string> repository);

    }
}