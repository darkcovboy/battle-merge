using UnityEngine;
using Input  = UnityEngine.Input;

namespace Game.Scripts.Battler.Input
{
    public class DesktopInput : IInput
    {
        public Vector3 GetInputDirection()
        {
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
    }
}