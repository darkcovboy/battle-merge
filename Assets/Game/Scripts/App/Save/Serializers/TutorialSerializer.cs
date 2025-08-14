using System;
using Game.Scripts.Modules.SaveLoad.Serializers;
using Game.Scripts.Modules.Tutorial;

namespace Game.Scripts.App.Save.Serializers
{
    public class TutorialSerializer : GameSerializer<TutorialService, TutorialData>
    {
        protected override TutorialData Serialize(TutorialService service)
        {
            return new TutorialData
            {
                IsCompleted = service.IsCompleted
            };
        }

        protected override void Deserialize(TutorialService service, TutorialData data)
        {
            service.Setup(data.IsCompleted);
        }
    }

    [Serializable]
    public struct TutorialData
    {
        public bool IsCompleted;
    }
}