using UnityEngine;

namespace Game.Scripts.Battler.Player.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new(0, 10, -10);
        [SerializeField] private float _smoothSpeed = 5f;

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Vector3 desiredPosition = _target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
            transform.LookAt(_target);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

    }
}