using Game.Scripts.Battler.Enemies.StateMachine.States;

namespace Game.Scripts.Battler.Enemies.StateMachine
{
    public class EnemyStateMachine
    {
        private readonly Module.StateMachine.StateMachine _stateMachine = new();
        private readonly EnemyController _enemy;
        private readonly EnemyView _view;
        
        private readonly EnemyPatrolState _patrol;
        private readonly EnemyBattleState _battle;
        private readonly EnemyDeadState _dead;
        private readonly EnemyVictoryState _victory;
        
        public EnemyStateMachine(EnemyController enemy, EnemyView view)
        {
            _enemy = enemy;
            _view = view;

            _patrol = new EnemyPatrolState(_stateMachine, _enemy, _view);
            _battle = new EnemyBattleState(_stateMachine, _enemy, _view);
            _dead = new EnemyDeadState(_stateMachine, _enemy, _view);
            _victory = new EnemyVictoryState(_stateMachine, _enemy, _view);

            _stateMachine.ChangeState(_patrol);
        }
        
        public void Update() => _stateMachine.Update();

        public void ChangeState(EnemyStateType state)
        {
            switch (state)
            {
                case EnemyStateType.Patrol: _stateMachine.ChangeState(_patrol); break;
                case EnemyStateType.Battle: _stateMachine.ChangeState(_battle); break;
                case EnemyStateType.Dead: _stateMachine.ChangeState(_dead); break;
                case EnemyStateType.Victory: _stateMachine.ChangeState(_victory); break;
            }

        }
        public enum EnemyStateType
        {
            Patrol,
            Battle,
            Dead,
            Victory
        }

    }
}