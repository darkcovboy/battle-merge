using System;
using UnityEngine;

namespace Game.Scripts.Battler.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        private static readonly int Throw = Animator.StringToHash("Throw");

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

        public void PlayThrow()
        {
            _animator.SetTrigger(Throw);
        }

        // вызывать из Animation Event
        public void OnThrowEvent()
        {
            OnThrowAnimationComplete?.Invoke();
        }
    }
}