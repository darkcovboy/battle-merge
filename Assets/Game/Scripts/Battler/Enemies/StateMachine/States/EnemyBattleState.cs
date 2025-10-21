using Game.Scripts.Battler.Module.StateMachine;

namespace Game.Scripts.Battler.Enemies.StateMachine.States
{
    public class EnemyBattleState : BaseState
    {
        private readonly EnemyController _enemy;
        private readonly EnemyView _view;

        public EnemyBattleState(Module.StateMachine.StateMachine sm, EnemyController enemy, EnemyView view) : base(sm)
        {
            _enemy = enemy;
            _view = view;
        }

        public override void Enter()
        {
            _view.SetIdle();
        }

        public override void Update() { }

    }
}