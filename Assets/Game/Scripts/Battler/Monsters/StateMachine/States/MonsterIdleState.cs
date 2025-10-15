using Game.Scripts.Battler.Module.StateMachine;

namespace Game.Scripts.Battler.Monsters.StateMachine.States
{
    public class MonsterIdleState : BaseState
    {
        private readonly MonsterCharacter _monster;
        private readonly MonsterView _view;

        public MonsterIdleState(Module.StateMachine.StateMachine stateMachine, MonsterCharacter monster, MonsterView view)
            : base(stateMachine)
        {
            _monster = monster;
            _view = view;
        }

        public override void Enter() => _view.SetIdle();

    }
}