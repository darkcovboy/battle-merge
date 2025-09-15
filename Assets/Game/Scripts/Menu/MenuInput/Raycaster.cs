using System;
using Game.Scripts.Menu.Characters;
using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.MenuInput
{
    public class Raycaster : IInitializable, IDisposable
    {
        private readonly IMenuInput _input;
        private readonly Camera _camera;
        private readonly LayerMask _characterLayer;
        private readonly LayerMask _cellLayer;
        
        private IDraggableCharacter _currentCharacter;
        private Cell _hoverCell;


        public Raycaster(IMenuInput input, Camera camera)
        {
            Debug.Log("Constructing Raycaster");
            _input = input;
            _camera = camera;
            
            _characterLayer = LayerMask.GetMask("DraggableCharacter");
            _cellLayer = LayerMask.GetMask("Cell");
        }
        
        public void Initialize()
        {
            Debug.Log($"Initializing {nameof(Raycaster)}...");
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
            Debug.Log("OnPressed");
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out var hit, 100f, _characterLayer))
            {
                
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

            _currentCharacter.OnDrag(screenPos);

            if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out var hit, 100f, _cellLayer))
            {
                if (hit.collider.TryGetComponent(out Cell cell))
                {
                    if (_hoverCell != cell)
                    {
                        ResetHover();
                        _hoverCell = cell;
                        _hoverCell.SetPlaceable();
                    }
                }
            }
            else
            {
                ResetHover();
            }
        }

        private void OnReleased()
        {
            if (_currentCharacter != null)
            {
                _currentCharacter.OnDrop(_hoverCell);
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