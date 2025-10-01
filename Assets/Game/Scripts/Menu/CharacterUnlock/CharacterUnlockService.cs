using System;
using System.Collections.Generic;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.App.Characters.Menu;
using Game.Scripts.Menu.Field;

namespace Game.Scripts.Menu.CharacterUnlock
{
    public interface ICharacterUnlockService
    {
        public HashSet<string> CharactersUnlocked { get; }
        event Action<CharacterConfig> CharacterUnlocked;
    }

    public class CharacterUnlockService : IDisposable, ICharacterUnlockService
    {
        private readonly IGameEventMediator _gameEventMediator;
        public event Action<CharacterConfig> CharacterUnlocked;

        public HashSet<string> CharactersUnlocked { get; private set; }

        public CharacterUnlockService(IGameEventMediator gameEventMediator)
        {
            _gameEventMediator = gameEventMediator;
            _gameEventMediator.CharacterMerged += CheckAndUpdateUnlock;
        }

        private void CheckAndUpdateUnlock(CharacterConfig config)
        {
            if (CharactersUnlocked.Add(config.NameId))
            {
                CharacterUnlocked?.Invoke(config);
            }
        }

        public void Dispose()
        {
            _gameEventMediator.CharacterMerged -= CheckAndUpdateUnlock;
        }

        public void Setup(HashSet<string> dataUnlockedCharacters)
        {
            CharactersUnlocked = dataUnlockedCharacters;
        }
    }
}