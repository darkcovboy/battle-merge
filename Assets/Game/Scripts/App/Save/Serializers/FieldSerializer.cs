using System;
using System.Collections.Generic;
using Game.Scripts.Menu.Field;
using Game.Scripts.Modules.SaveLoad.Serializers;

namespace Game.Scripts.App.Save.Serializers
{
    public class FieldSerializer : GameSerializer<FieldPresenter, CharactersPositions>
    {
        protected override CharactersPositions Serialize(FieldPresenter service)
        {
            return new CharactersPositions
            {
                Items = new Dictionary<int, string>(service.CharacterPositions)
            };

        }

        protected override void Deserialize(FieldPresenter service, CharactersPositions data)
        {
            service.Setup(data.Items);
        }
        
        protected override void SetupByDefault(FieldPresenter service)
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