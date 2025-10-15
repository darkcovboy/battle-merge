using UnityEngine;

namespace Game.Scripts.Battler.Monsters
{
    public interface IMonsterTarget
    {
        Transform Transform { get; }
        void TakeDamage(float damage);
    }
}