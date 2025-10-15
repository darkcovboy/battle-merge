using UnityEngine;

namespace Game.Scripts.Battler.Input
{
    public interface IInput
    {
        public Vector3 GetInputDirection();
        void SetBlocked(bool isBlocked);
        bool IsBlocked { get; }
    }
}