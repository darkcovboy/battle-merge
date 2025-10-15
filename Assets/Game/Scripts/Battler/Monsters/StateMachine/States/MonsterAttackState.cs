using Game.Scripts.App.Characters.Data;
using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters.StateMachine.States
{
    public class MonsterAttackState : BaseState
    {
        private readonly MonsterCharacter _monster;
        private readonly MonsterView _view;
        private float _cooldownTimer;

        public MonsterAttackState(Module.StateMachine.StateMachine stateMachine, MonsterCharacter monster, MonsterView view)
            : base(stateMachine)
        {
            _monster = monster;
            _view = view;
        }

        public override void Enter() => _cooldownTimer = 0;

        public override void Update()
        {
            var target = _monster.GetTarget();
            if (target == null) return;

            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _view.PlayAttack();
                if (_monster.Config.WarriorType == CharacterWarriorType.Fighter)
                    target.TakeDamage(_monster.Config.Damage);
                else
                    ShootProjectile(target);
                _cooldownTimer = _monster.Config.AttackCooldown;
            }
        }

        private void ShootProjectile(IMonsterTarget target)
        {
            if (_monster.ProjectilePrefab == null) return;

            var proj = Object.Instantiate(_monster.ProjectilePrefab,
                _view.ProjectileSpawnPosition.position, Quaternion.identity);
        }
    }
}