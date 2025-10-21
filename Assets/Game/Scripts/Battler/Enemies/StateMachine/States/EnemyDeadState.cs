using DG.Tweening;
using Game.Scripts.Battler.Module.StateMachine;

namespace Game.Scripts.Battler.Enemies.StateMachine.States
{
    public class EnemyDeadState : BaseState
    {
        private readonly EnemyController _enemy;
        private readonly EnemyView _view;

        public EnemyDeadState(Module.StateMachine.StateMachine sm, EnemyController enemy, EnemyView view) : base(sm)
        {
            _enemy = enemy;
            _view = view;
        }

        public override void Enter()
        {
            _view.PlayDeath();
        }
    }

    public class EnemyVictoryState : BaseState
    {
        private readonly EnemyView _view;

        public EnemyVictoryState(Module.StateMachine.StateMachine sm, EnemyController enemy, EnemyView view) : base(sm)
        {
            _view = view;
        }

        public override void Enter()
        {
            _view.PlayVictory();
        }

    }
}