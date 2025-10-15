using UnityEngine;
using Input  = UnityEngine.Input;

namespace Game.Scripts.Battler.Input
{
    public class DesktopInput : IInput
    {
        private bool _isBlocked;

        public Vector3 GetInputDirection()
        {
            if (_isBlocked) return Vector3.zero;

            float horizontalInput = SimpleInput.GetAxis("Horizontal");
            float verticalInput = SimpleInput.GetAxis("Vertical");

            if(horizontalInput == 0 && verticalInput == 0)
            {
                horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
                verticalInput = UnityEngine.Input.GetAxis("Vertical");
            }

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            return moveDirection;

        }

        public bool IsBlocked => _isBlocked;
        public void SetBlocked(bool isBlocked) => _isBlocked = isBlocked;

    }
}