using System;
using UnityEngine;

namespace Game.Scripts.Battler.Player
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Throw = Animator.StringToHash("Throw");
        
        public event Action OnThrowAnimationComplete;

        
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed = 6f;
        [SerializeField] private Transform _pokeballPosition;
        [SerializeField] private Transform _pokeballModel;
        
        public Transform PokeballPosition => _pokeballPosition;
        
        public void SetIdle()
        {
            _animator.SetFloat(Speed, 0);
        }
        
        public void PlayThrow()
        {
            _pokeballModel.gameObject.SetActive(true);
            _animator.SetTrigger(Throw);
        }
        
        public void OnThrowAnimationFinished()
        {
            _pokeballModel.gameObject.SetActive(false);
            OnThrowAnimationComplete?.Invoke();
        }

        public void Move(Vector3 dirNormalized)
        {
            if (dirNormalized.sqrMagnitude < 0.01f)
            {
                SetIdle();
                return;
            }

            _animator.SetFloat(Speed, _speed);

            Quaternion targetRotation = Quaternion.LookRotation(dirNormalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            transform.position += dirNormalized * (Time.deltaTime * _speed);
        }
    }
}