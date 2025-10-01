using System;
using Game.Scripts.App.Characters.Data;

namespace Game.Scripts.Menu
{
    public interface IGameEventMediator
    {
        event Action<CharacterConfig> CharacterMerged;
        void NotifyCharacterMerged(CharacterConfig config);
    }

    public class GameEventMediator : IGameEventMediator
    {
        public event Action<CharacterConfig> CharacterMerged;
    
        public void NotifyCharacterMerged(CharacterConfig config)
        {
            CharacterMerged?.Invoke(config);
        }
    }
}