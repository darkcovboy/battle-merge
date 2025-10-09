using UnityEngine;

namespace Game.Scripts.Battler.Player
{
    public class PlayerView : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed = 6f;
        
        public void SetIdle()
        {
            _animator.SetFloat(Speed, 0);
        }

        public void Move(Vector3 dirNormalized)
        {
            _animator.SetFloat(Speed, _speed);
            transform.rotation = Quaternion.LookRotation(dirNormalized);
            transform.Translate(dirNormalized *Time.deltaTime * _speed, Space.World);
        }
    }
}