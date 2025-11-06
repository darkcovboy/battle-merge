using System.Collections.Generic;
using Game.Scripts.Menu.UI.Shop;
using Game.Scripts.Modules.SaveLoad.Serializers;

namespace Game.Scripts.App.Save.Serializers
{
    public class SkinSerializer : GameSerializer<SkinService, SkinData>
    {
        protected override SkinData Serialize(SkinService service)
        {
            return new SkinData()
            {
                CurrentSkinID = service.CurrentSkin,
                UnlockedSkinIDs = service.UnlockedSkins
            };
        }

        protected override void Deserialize(SkinService service, SkinData data)
        {
            service.Setup(data.CurrentSkinID, data.UnlockedSkinIDs);
        }

        protected override void SetupByDefault(SkinService service)
        {
            service.Setup(0,new List<int>{0});
        }
    }

    public struct SkinData
    {
        public int CurrentSkinID;
        public List<int> UnlockedSkinIDs;
    }
}