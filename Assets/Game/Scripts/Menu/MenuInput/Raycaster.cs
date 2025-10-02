using System;
using Game.Scripts.App.Characters.Menu;
using Game.Scripts.Menu.Field.CellScripts;
using Game.Scripts.Menu.Trash;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.MenuInput
{
    public class Raycaster : IInitializable, IDisposable, IRaycaster
    {
        public event Action<IDraggableCharacter> OnPick;
        public event Action<Cell, IDraggableCharacter> Dropped;
        public event Action<Cell, IDraggableCharacter> Hovered;
        public event Action<IDraggableCharacter> OnTrash;
        public event Action HoverExited;

        private readonly IMenuInput _input;
        private readonly Camera _camera;
        private readonly LayerMask _characterLayer;
        private readonly LayerMask _cellLayer;
        private readonly int _trashLayer;

        private IDraggableCharacter _currentCharacter;
        private Cell _hoverCell;
        private bool _isOverTrash;


        public Raycaster(IMenuInput input, Camera camera)
        {
            _input = input;
            _camera = camera;
            
            _characterLayer = LayerMask.GetMask("DraggableCharacter");
            _cellLayer = LayerMask.GetMask("Cell");
            _trashLayer = LayerMask.GetMask("Trash");
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
            if (Physics.Raycast(_camera.ScreenPointToRay(screenPos), out var hit, 1000f, _characterLayer))
            {
                if (hit.collider.TryGetComponent(out IDraggableCharacter character))
                {
                    _currentCharacter = character;
                    _currentCharacter.OnPick();
                    OnPick?.Invoke(_currentCharacter);
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
            
            _isOverTrash = Physics.Raycast(ray, out hit, 100f, _trashLayer);
            //Добавить красную обводку, чтобы было понятно, что мы над мусоркой
            /*
            bool wasOverTrash = _isOverTrash;
            _isOverTrash = Physics.Raycast(ray, out hit, 100f, _trashLayer);
            
            if (_isOverTrash != wasOverTrash)
            {
                _currentCharacter?.SetTrashHighlight(_isOverTrash);
            }
            */
        }

        private void OnReleased()
        {
            if (_currentCharacter != null)
            {
                if (_isOverTrash)
                {
                    OnTrash?.Invoke(_currentCharacter);
                    ResetHover();
                }
                else
                {
                    Dropped?.Invoke(_hoverCell, _currentCharacter);
                }
                
                _currentCharacter = null;
            }

            _isOverTrash = false;
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