using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Menu.Characters;
using Game.Scripts.Menu.Field.CellScripts;
using Game.Scripts.Menu.MenuInput;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field
{
    public class FieldPresenter : IInitializable, IDisposable
    {
        private readonly FieldView _fieldView;
        private readonly CellFactory _cellFactory;
        private readonly IRaycaster _raycaster;
        private readonly List<Cell> _cellViews = new List<Cell>();
        private readonly Dictionary<int, IDraggableCharacter> _draggableCharacters = new Dictionary<int, IDraggableCharacter>(); 
        public IDictionary<int,string> CharacterPositions { get; private set; }

        public FieldPresenter(FieldView fieldView,
            CellFactory cellFactory,
            IRaycaster raycaster)
        {
            _fieldView = fieldView;
            _cellFactory = cellFactory;
            _raycaster = raycaster;
        }

        public void Setup(Dictionary<int, string> dataItems)
        {
            CharacterPositions = dataItems;
        }

        public void Initialize()
        {
            _raycaster.Dropped += OnCharacterDropped;
            for (int i = 0; i < 16; i++)
            {
                Cell cell = _cellFactory.Create(_fieldView.Root, i);
                _cellViews.Add(cell);

                if (!string.IsNullOrEmpty(CharacterPositions[i]))
                {
                    //Спавним персонажа
                }
            }
        }

        public void Dispose()
        {
            _raycaster.Dropped -= OnCharacterDropped;
        }

        private void OnCharacterDropped(Cell cell, IDraggableCharacter character)
        {
            if (cell == null)
            {
                character.OnDrop(cell);
                return;
            }

            int cellId = cell.Id;
            string charId = character.Id;

            if (string.IsNullOrEmpty(CharacterPositions[cellId]))
            {
                CharacterPositions[cellId] = charId;
                _draggableCharacters[cellId] = character;
                //cell.SetOccupiedVisual();
            }
            else if (CanMerge(cellId, charId))
            {
                //MergeCharacters(character, _draggableCharacters[cellId]);
            }
            else
            {
                Cell cellCurrentCharacter = _cellViews.Find(cell => cell.Id == character.PositionId);
                character.OnDrop(cellCurrentCharacter);
            }
        }

        private bool CanMerge(int id, string characterId) => CharacterPositions[id].Equals(characterId);
    }
}