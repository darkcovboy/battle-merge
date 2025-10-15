using Game.Scripts.App.Characters.Data;
using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters.StateMachine.States
{
    public class MonsterFollowState : BaseState
    {
        private readonly MonsterCharacter _monster;
        private readonly MonsterView _view;

        public MonsterFollowState(Module.StateMachine.StateMachine stateMachine, MonsterCharacter monster, MonsterView view)
            : base(stateMachine)
        {
            _monster = monster;
            _view = view;
        }

        public override void Update()
        {
            var target = _monster.GetTarget();
            if (target == null || target.Transform == null)
            {
                _monster.SetTarget(null);
                _view.SetIdle();
                return;
            }
            
            Vector3 dir = (target.Transform.position - _monster.Transform.position);
            float distance = dir.magnitude;

            if (_monster.Config.WarriorType == CharacterWarriorType.Shooter)
            {
                float desiredDistance = _monster.Config.AttackRange * 0.8f;

                if (distance <= desiredDistance)
                {
                    _view.SetIdle();
                    _monster.Transform.rotation = Quaternion.LookRotation(dir.normalized);
                    return; 
                }
            }

            dir.Normalize();
            _monster.Transform.position += dir * (_monster.Config.MoveSpeed * Time.deltaTime);
            _monster.Transform.rotation = Quaternion.LookRotation(dir);
            _view.SetMove(_monster.Config.MoveSpeed);
        }
    }
}