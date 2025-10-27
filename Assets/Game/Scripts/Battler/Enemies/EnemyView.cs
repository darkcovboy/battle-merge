using System;
using UnityEngine;

namespace Game.Scripts.Battler.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Throw = Animator.StringToHash("Throw");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Win = Animator.StringToHash("Win");

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [field: SerializeField] public Transform PokeballPosition { get; private set; }

        public event Action OnThrowAnimationComplete;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_animator == null)
                _animator = GetComponentInChildren<Animator>();
        }
#endif

        public void Move(Vector3 targetPos)
        {
            _characterController.Move(targetPos);
        }
        public void SetIdle() => _animator.SetFloat(Speed, 0);
        public void SetMove(float speed) => _animator.SetFloat(Speed, speed);
        public void PlayThrow() => _animator.SetTrigger(Throw);
        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayVictory() => _animator.SetTrigger(Win);

        public void OnThrowEvent() => OnThrowAnimationComplete?.Invoke();
    }
}