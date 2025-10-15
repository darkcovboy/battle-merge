using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters.StateMachine
{
    public class HasTargetCondition : ITransition
    {
        private readonly MonsterCharacter _monster;
        public HasTargetCondition(MonsterCharacter monster) => _monster = monster;

        public bool IsMet() => _monster.GetTarget() != null && !_monster.IsDead;
    }

    public class TargetInRangeCondition : ITransition
    {
        private readonly MonsterCharacter _monster;
        public TargetInRangeCondition(MonsterCharacter monster) => _monster = monster;

        public bool IsMet()
        {
            var t = _monster.GetTarget();
            if (t == null) return false;
            return Vector3.Distance(_monster.Transform.position, t.Transform.position) <= _monster.Config.AttackRange;
        }
    }

    public class TargetOutOfRangeCondition : ITransition
    {
        private readonly MonsterCharacter _monster;
        public TargetOutOfRangeCondition(MonsterCharacter monster) => _monster = monster;

        public bool IsMet()
        {
            var t = _monster.GetTarget();
            if (t == null) return false;
            return Vector3.Distance(_monster.Transform.position, t.Transform.position) > _monster.Config.AttackRange;
        }
    }

    public class MonsterDeadCondition : ITransition
    {
        private readonly MonsterCharacter _monster;
        public MonsterDeadCondition(MonsterCharacter monster) => _monster = monster;
        public bool IsMet() => _monster.IsDead;
    }

}