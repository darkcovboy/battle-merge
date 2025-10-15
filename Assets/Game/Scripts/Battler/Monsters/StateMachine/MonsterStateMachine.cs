using Game.Scripts.Battler.Module.StateMachine;
using Game.Scripts.Battler.Monsters.StateMachine.States;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters.StateMachine
{
    public class MonsterStateMachine
    {
        private readonly Module.StateMachine.StateMachine _stateMachine = new();
        private readonly MonsterCharacter _monster;
        private readonly MonsterView _view;

        private readonly MonsterIdleState _idleState;
        private readonly MonsterFollowState _followState;
        private readonly MonsterAttackState _attackState;
        private readonly MonsterDeadState _deadState;

        public MonsterStateMachine(MonsterCharacter monster, MonsterView view)
        {
            _monster = monster;
            _view = view;

            _idleState = new MonsterIdleState(_stateMachine, _monster, _view);
            _followState = new MonsterFollowState(_stateMachine, _monster, _view);
            _attackState = new MonsterAttackState(_stateMachine, _monster, _view);
            _deadState = new MonsterDeadState(_stateMachine, _monster, _view);

            _idleState.AddTransition(_followState, new HasTargetCondition(_monster));
            _followState.AddTransition(_attackState, new TargetInRangeCondition(_monster));
            _attackState.AddTransition(_followState, new TargetOutOfRangeCondition(_monster));
            
            _idleState.AddTransition(_deadState, new MonsterDeadCondition(_monster));
            _followState.AddTransition(_deadState, new MonsterDeadCondition(_monster));
            _attackState.AddTransition(_deadState, new MonsterDeadCondition(_monster));


            _stateMachine.ChangeState(_idleState);
        }

        public void Update() => _stateMachine.Update();

        public void ChangeState(MonsterStateType type)
        {
            switch (type)
            {
                case MonsterStateType.Idle: _stateMachine.ChangeState(_idleState); break;
                case MonsterStateType.Follow: _stateMachine.ChangeState(_followState); break;
                case MonsterStateType.Attack: _stateMachine.ChangeState(_attackState); break;
                case MonsterStateType.Dead: _stateMachine.ChangeState(_deadState); break;
            }
        }
    }
}

public enum MonsterStateType
{
    Idle,
    Follow,
    Attack,
    Dead
}