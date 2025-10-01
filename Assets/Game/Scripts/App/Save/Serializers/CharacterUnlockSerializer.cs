using System.Collections.Generic;
using Game.Scripts.Menu.CharacterUnlock;
using Game.Scripts.Modules.SaveLoad.Serializers;

namespace Game.Scripts.App.Save.Serializers
{
    public class CharacterUnlockSerializer : GameSerializer<CharacterUnlockService, CharactersUnlock>
    {
        protected override CharactersUnlock Serialize(CharacterUnlockService service)
        {
            return new CharactersUnlock()
            {
                UnlockedCharacters = service.CharactersUnlocked
            };
        }

        protected override void Deserialize(CharacterUnlockService service, CharactersUnlock data)
        {
            service.Setup(data.UnlockedCharacters);
        }

        protected override void SetupByDefault(CharacterUnlockService service)
        {
            var dict = new HashSet<string>();

            service.Setup(dict);
        }
    }

    public struct CharactersUnlock
    {
        public HashSet<string> UnlockedCharacters;
    }
}