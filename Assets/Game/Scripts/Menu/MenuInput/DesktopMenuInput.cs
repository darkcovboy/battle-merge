using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.MenuInput

{
    public class DesktopMenuInput : IMenuInput, ITickable
    {
        private const int LeftMouseButtonIndex = 0;
        private readonly Camera _camera;
        
        public event Action<Vector3> Pressed;
        public event Action<Vector3> Move;
        public event Action Released;

        public DesktopMenuInput(Camera camera)
        {
            _camera = camera;
        }
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(LeftMouseButtonIndex))
                Pressed?.Invoke(Input.mousePosition);

            if (Input.GetMouseButtonUp(LeftMouseButtonIndex))
                Released?.Invoke();

            Move?.Invoke(Input.mousePosition);
        }
    }
}