using UnityEngine;

namespace Game.Scripts.Battler.Input
{
    public class MobileInput : IInput
    {
        private bool _isBlocked;

        public Vector3 GetInputDirection()
        {
            if (_isBlocked) return Vector3.zero;

            float horizontalInput = SimpleInput.GetAxis("Horizontal");
            float verticalInput = SimpleInput.GetAxis("Vertical");
            
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            return moveDirection;
        }
        public bool IsBlocked => _isBlocked;
        public void SetBlocked(bool isBlocked) => _isBlocked = isBlocked;

    }
}