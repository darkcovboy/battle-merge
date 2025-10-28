using Game.Scripts.App.GameScenes;
using Game.Scripts.Modules.SaveLoad.Serializers;

namespace Game.Scripts.App.Save.Serializers
{
    public class GameScenesSerializer : GameSerializer<GameSceneManager, GameScenes>
    {
        protected override GameScenes Serialize(GameSceneManager service)
        {
            return new GameScenes()
            {
                SceneIndex = service.CurrentLevel,
                SceneUI = service.CurrentUILevel
            };
        }

        protected override void Deserialize(GameSceneManager service, GameScenes data)
        {
            service.Setup(data.SceneIndex, data.SceneUI);
        }

        protected override void SetupByDefault(GameSceneManager service)
        {
            service.Setup(1, 1);
        }
    }

    public struct GameScenes
    {
        public int SceneIndex;
        public int SceneUI;
    }
}