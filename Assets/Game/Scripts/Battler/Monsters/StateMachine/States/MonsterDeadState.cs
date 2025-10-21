using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters.StateMachine.States
{
    public class MonsterDeadState : BaseState
    {
        private readonly MonsterView _view;
        private readonly MonsterCharacter _monster;

        public MonsterDeadState(Module.StateMachine.StateMachine stateMachine, MonsterCharacter monster, MonsterView view)
            : base(stateMachine)
        {
            _monster = monster;
            _view = view;
        }

        public override void Enter()
        {
            _view.PlayDeath();
        }
    }
}