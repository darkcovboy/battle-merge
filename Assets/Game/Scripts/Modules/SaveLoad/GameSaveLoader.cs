using System.Collections.Generic;
using Game.Scripts.Modules.SaveLoad.Repository;
using Game.Scripts.Modules.SaveLoad.Serializers;
using Sirenix.OdinInspector;

namespace Game.Scripts.Modules.SaveLoad
{
    public class GameSaveLoader
    {
        private readonly IGameRepository _repository;
        private readonly IGameSerializer[] _orderedSerializators;

        public GameSaveLoader(IGameRepository repository, IGameSerializer[] orderedSerializators)
        {
            _repository = repository;
            _orderedSerializators = orderedSerializators;
        }

        public void SetGameData(Dictionary<string, string> repository)
        {
            if (repository != null)
            {
                _repository.SetRepository(repository);
            }
        }

        public void Save(bool isForcePush = false)
        {
            var gameState = new Dictionary<string, string>();

            foreach (IGameSerializer serializer in _orderedSerializators)
                serializer.Serialize(gameState);

            _repository.SetState(gameState);
            //SaveSystem.PlayerData.GameState = gameState;
            //EccentricInit.Instance.SaveSystem.Save(SaveSystem.PlayerData, isForcePush);
        }

        public void Load()
        {
            Dictionary<string, string> gameState = _repository.GetState();

            if (gameState.Count == 0)
            {
                foreach (IGameSerializer serializer in _orderedSerializators)
                    serializer.SetupDefault();

                Save();
            }

            foreach (IGameSerializer serializer in _orderedSerializators)
                serializer.Deserialize(gameState);
        }
    }
}