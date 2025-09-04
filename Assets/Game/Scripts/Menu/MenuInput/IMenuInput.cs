using System;
using UnityEngine;

namespace Game.Scripts.Menu.MenuInput
{
    public interface IMenuInput
    {
        public event Action<Vector3> Pressed;
        public event Action<Vector3> Move;
        public event Action Released;
    }
}