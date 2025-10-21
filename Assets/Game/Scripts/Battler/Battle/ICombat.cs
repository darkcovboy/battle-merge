using System.Collections.Generic;
using Game.Scripts.Battler.Monsters;

namespace Game.Scripts.Battler.Battle
{
    public interface ICombat
    {
        IReadOnlyList<MonsterCharacter> Team { get; }

        void OnBattleStarted(ICombat opponent);
        void OnBattleEnded(bool victory);
    }
}