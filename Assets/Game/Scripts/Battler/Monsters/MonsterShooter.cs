using Game.Scripts.Battler.Monsters.Projectiles;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters
{
    public class MonsterShooter : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectilePrefab;
        
        private MonsterCharacter _owner;

        public void Initialize(MonsterCharacter owner)
        {
            _owner = owner;
        }

        public void OnAttackEvent()
        {
            if (_owner == null || _owner.IsDead) return;
            
            var target = _owner.GetTarget();
            if (target == null) return;

            var projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
            projectile.Launch(target, _owner.Config.Damage);
        }
    }
}