using UnityEngine;

namespace Game.Scripts.Battler.Input
{
    public class MobileInput : IInput
    {
        public Vector3 GetInputDirection()
        {
            float horizontalInput = SimpleInput.GetAxis("Horizontal");
            float verticalInput = SimpleInput.GetAxis("Vertical");
            
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            return moveDirection;
        }
    }
}