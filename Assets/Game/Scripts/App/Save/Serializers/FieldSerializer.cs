using System.Collections.Generic;
using Game.Scripts.Menu.Field;
using Game.Scripts.Modules.SaveLoad.Serializers;

namespace Game.Scripts.App.Save.Serializers
{
    public class FieldSerializer : GameSerializer<Field, CharactersPositions>
    {
        protected override CharactersPositions Serialize(Field service)
        {
            return new CharactersPositions
            {
                Items = new Dictionary<int, string>(service.CharacterPositions)
            };
        }

        protected override void Deserialize(Field service, CharactersPositions data)
        {
            service.Setup(data.Items);
        }
        
        protected override void SetupByDefault(Field service)
        {
            var dict = new Dictionary<int, string>();
            for (int i = 0; i < 16; i++)
                dict[i] = string.Empty;

            service.Setup(dict);
        }
    }

    public struct CharactersPositions
    {
        public Dictionary<int, string> Items;
    }
}