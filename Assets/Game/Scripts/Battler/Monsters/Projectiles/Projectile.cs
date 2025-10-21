using UnityEngine;

namespace Game.Scripts.Battler.Monsters.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxLifetime = 5f;
        [SerializeField] private float _hitRadius = 0.2f;

        private IMonsterTarget _target;
        private float _damage;
        private bool _isLaunched;
        private Vector3 _lastKnownTargetPosition;

        public void Launch(IMonsterTarget target, float damage)
        {
            _target = target;
            _damage = damage;
            _isLaunched = true;
            _lastKnownTargetPosition = target?.Transform.position ?? transform.position + transform.forward * 10f;

            Destroy(gameObject, _maxLifetime);
        }

        private void Update()
        {
            if (!_isLaunched) return;

            if (_target != null)
                _lastKnownTargetPosition = _target.Transform.position;

            Vector3 dir = (_lastKnownTargetPosition - transform.position).normalized;
            transform.position += dir * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(dir);

            if (Vector3.Distance(transform.position, _lastKnownTargetPosition) <= _hitRadius)
            {
                if (_target != null)
                    _target.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
    }
}