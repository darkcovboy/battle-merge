using UnityEngine;

namespace Game.Scripts.Battler.Monsters
{
    public class MonsterView : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField] private Animator _animator;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_animator == null)
                _animator = GetComponentInChildren<Animator>();
        }
#endif

        public void SetIdle() => _animator.SetFloat(Speed, 0);
        public void SetMove(float speed) => _animator.SetFloat(Speed, speed);
        public void PlayAttack() => _animator.SetTrigger(Attack);
        public void PlayDeath() => _animator.SetTrigger(Die);

    }
}