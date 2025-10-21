using System.Collections.Generic;
using Game.Scripts.Battler.Monsters;
using UnityEngine;

namespace Game.Scripts.Battler.Battle
{
    public interface ICombat
    {
        IReadOnlyList<MonsterCharacter> Team { get; }

        Transform Transform { get; }
        bool IsInBattle { get; set; }

        void OnBattleStarted(ICombat opponent);
        void OnBattleEnded(bool victory);
    }
}