using System;
using Game.Scripts.Menu.Characters;
using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.MenuInput
{
    public class Raycaster : IInitializable, IDisposable, IRaycaster
    {
        public event Action<Cell, IDraggableCharacter> Dropped;
        public event Action<Cell, IDraggableCharacter> Hovered;
        public event Action HoverExited;

        private readonly IMenuInput _input;
        private readonly Camera _camera;
        private readonly LayerMask _characterLayer;
        private readonly LayerMask _cellLayer;
        
        private IDraggableCharacter _currentCharacter;
        private Cell _hoverCell;
        

        public Raycaster(IMenuInput input, Camera camera)
        {
            _input = input;
            _camera = camera;
            
            _characterLayer = LayerMask.GetMask("DraggableCharacter");
            _cellLayer = LayerMask.GetMask("Cell");
        }
        
        public void Initialize()
        {
            _input.Pressed += OnPressed;
            _input.Move += OnMove;
            _input.Released += OnReleased;
        }

        public void Dispose()
        {
            _input.Pressed -= OnPressed;
            _input.Move -= OnMove;
            _input.Released -= OnReleased;
        }
        
        private void OnPressed(Vector3 screenPos)
        {
            Debug.Log($"OnPressed: {_characterLayer.value}");
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out var hit, 1000f, _characterLayer))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.TryGetComponent(out IDraggableCharacter character))
                {
                    _currentCharacter = character;
                    _currentCharacter.OnPick();
                }
            }
        }
        
        private void OnMove(Vector3 screenPos)
        {
            if (_currentCharacter == null)
                return;

            Ray ray = _camera.ScreenPointToRay(screenPos);

            Plane plane = new Plane(Vector3.up, new Vector3(0, 1.05f, 0));

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 worldPos = ray.GetPoint(enter);
                _currentCharacter.OnDrag(worldPos);
            }
            
            if (Physics.Raycast(ray, out var hit, 100f, _cellLayer))
            {
                if (hit.collider.TryGetComponent(out Cell cell))
                {
                    if (_hoverCell != cell)
                    {
                        ResetHover();
                        _hoverCell = cell;
                        Hovered?.Invoke(cell, _currentCharacter);
                    }
                }
            }
            else
            {
                if (_hoverCell != null)
                {
                    HoverExited?.Invoke();
                    _hoverCell = null;
                }

            }
        }

        private void OnReleased()
        {
            if (_currentCharacter != null)
            {
                Dropped?.Invoke(_hoverCell, _currentCharacter);
                _currentCharacter = null;
            }

            ResetHover();
        }

        private void ResetHover()
        {
            if (_hoverCell != null)
            {
                _hoverCell.SetDefault();
                _hoverCell = null;
            }
        }

    }
}