using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Enemies.StateMachine.States
{
    public class EnemyPatrolState : BaseState
    {
        private readonly EnemyController _enemy;
        private readonly EnemyView _view;
        private Vector3 _targetDir;
        private float _changeDirTimer;

        private float _speed = 2f;

        public EnemyPatrolState(Module.StateMachine.StateMachine stateMachine, EnemyController enemy, EnemyView view) : base(stateMachine)
        {
            _enemy = enemy;
            _view = view;
        }

        public override void Enter()
        {
            PickNewDirection();
        }

        public override void Update()
        {
            _changeDirTimer -= Time.deltaTime;

            if (_changeDirTimer <= 0f)
                PickNewDirection();

            Vector3 moveDirection = _targetDir * (_speed * Time.deltaTime);
            _view.Move(moveDirection);
            _view.SetMove(_speed);

            if (_targetDir != Vector3.zero)
            {
                var rot = Quaternion.LookRotation(_targetDir);
                _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, rot, Time.deltaTime * 3f);
            }
        }

        private void PickNewDirection()
        {
            _targetDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            _changeDirTimer = Random.Range(3f, 6f);
        }
    }
}