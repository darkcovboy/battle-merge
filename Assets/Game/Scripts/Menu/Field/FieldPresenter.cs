using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.App.Characters.Menu;
using Game.Scripts.Menu.Field.CellScripts;
using Game.Scripts.Menu.MenuInput;
using Game.Scripts.Modules.SaveLoad;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Scripts.Menu.Field
{
    public class FieldPresenter : IInitializable, IDisposable, IFieldPresenter
    {
        private readonly FieldView _fieldView;
        private readonly CellFactory _cellFactory;
        private readonly IRaycaster _raycaster;
        private readonly CharacterConfigCatalog _characterConfigCatalog;
        private readonly Field _field;
        private readonly GameSaveLoader _gameSaveLoader;
        private readonly List<Cell> _cellViews = new List<Cell>();
        private readonly Dictionary<int, IDraggableCharacter> _draggableCharacters = new Dictionary<int, IDraggableCharacter>();
        private IDictionary<int, string> CharacterPositions => _field.CharacterPositions;
        
        private Cell _lastHoveredCell;


        public FieldPresenter(FieldView fieldView,
            CellFactory cellFactory,
            IRaycaster raycaster,
            CharacterConfigCatalog characterConfigCatalog,
            Field field,
            GameSaveLoader gameSaveLoader)
        {
            _fieldView = fieldView;
            _cellFactory = cellFactory;
            _raycaster = raycaster;
            _characterConfigCatalog = characterConfigCatalog;
            _field = field;
            _gameSaveLoader = gameSaveLoader;
        }

        public void Initialize()
        {
            _raycaster.Dropped += OnCharacterDropped;
            _raycaster.Hovered += OnCharacterHovered;
            _raycaster.HoverExited += OnHoverExited;
            
            for (int i = 0; i < 16; i++)
            {
                Cell cell = _cellFactory.Create(_fieldView.Root, i);
                _cellViews.Add(cell);
            }

            CreateCharacters();
        }

        public void Dispose()
        {
            _raycaster.Dropped -= OnCharacterDropped;
            _raycaster.Hovered -= OnCharacterHovered;
            _raycaster.HoverExited -= OnHoverExited;
        }

        public void AddCharacter(string nameId, int cellId = -1)
        {
            CharacterConfig characterConfig = _characterConfigCatalog.CharacterConfigs
                .FirstOrDefault(c => c.NameId == nameId);

            if (characterConfig == null)
            {
                Debug.LogError($"CharacterConfig not found for nameId: {nameId}");
                return;
            }

            if (cellId == -1)
            {
                cellId = CharacterPositions
                    .FirstOrDefault(kvp => string.IsNullOrEmpty(kvp.Value))
                    .Key;

                if (!string.IsNullOrEmpty(CharacterPositions[cellId]))
                {
                    Debug.LogWarning("No free cells available for new character");
                    return;
                }
            }
            
            if (cellId < 0 || cellId >= _cellViews.Count)
            {
                Debug.LogError($"Invalid cellId: {cellId}");
                return;
            }
            
            Cell cell = _cellViews[cellId];

            GameObject instance = Object.Instantiate(
                characterConfig.ItemReference,
                cell.transform.position,
                Quaternion.identity,
                null
            );

            if (!instance.TryGetComponent<IDraggableCharacter>(out var draggable))
            {
                Debug.LogError($"ItemReference for {nameId} has no IDraggableCharacter component!");
                Object.Destroy(instance);
                return;
            }

            draggable.Appear();
            _draggableCharacters[cellId] = draggable;
            CharacterPositions[cellId] = nameId;
            draggable.PositionId = cellId;
            draggable.Config = characterConfig;
        }

        private async void CreateCharacters()
        {
            await UniTask.WaitForSeconds(0.02f);
            for (int i = 0; i < 16; i++)
            {
                if (!string.IsNullOrEmpty(CharacterPositions[i]))
                {
                    AddCharacter(_characterConfigCatalog.CharacterConfigs.Find(x=> x.NameId == CharacterPositions[i]).NameId, i);
                }
            }
        }

        private bool CanPlaceOrMerge(Cell cell, IDraggableCharacter character)
        {
            int cellId = cell.Id;
            
            if (cellId == character.PositionId)
                return true;

            if (string.IsNullOrEmpty(CharacterPositions[cellId]))
                return true;

            if (CanMerge(cellId, character.Config.NameId))
                return true;
            
            return false;
        }
        
        private void OnCharacterHovered(Cell cell, IDraggableCharacter character)
        {
            if (_lastHoveredCell != null && _lastHoveredCell != cell)
                _lastHoveredCell.SetDefault();

            _lastHoveredCell = cell;

            if (CanPlaceOrMerge(cell, character))
                cell.SetPlaceable();
            else
                cell.SetBlocked();
        }

        private void OnHoverExited()
        {
            if (_lastHoveredCell != null)
            {
                _lastHoveredCell.SetDefault();
                _lastHoveredCell = null;
            }
        }

        private void OnCharacterDropped(Cell cell, IDraggableCharacter character)
        {
            Cell cellCurrentCharacter = _cellViews.Find(cell => cell.Id == character.PositionId);
            
            if (cell == null)
            {
                character.OnDrop(cellCurrentCharacter, true);
                return;
            }

            int newCellId = cell.Id;
            int oldCellId = character.PositionId;
            string charId = character.Config.NameId;

            if (newCellId == oldCellId)
            {
                character.OnDrop(cellCurrentCharacter, false);
                return; 
            }
            
            if (string.IsNullOrEmpty(CharacterPositions[newCellId]))
            {
                CharacterPositions[oldCellId] = string.Empty;
                _draggableCharacters.Remove(oldCellId);

                CharacterPositions[newCellId] = charId;
                _draggableCharacters[newCellId] = character;

                character.PositionId = newCellId;
                character.OnDrop(cell, false);
            }
            else if (CanMerge(newCellId, charId, out var nextConfig))
            {
                MergeCharacters(cell,character, _draggableCharacters[newCellId],nextConfig.NameId);
            }
            else
            {
                character.OnDrop(cellCurrentCharacter, true);
            }

            _gameSaveLoader.Save();
        }

        private void MergeCharacters(Cell cell, IDraggableCharacter charA, IDraggableCharacter charB, string newCharId)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(charA.Disappear());
            sequence.Join(charB.Disappear());

            sequence.OnComplete(() =>
            {
                charA.DestroyCharacter();
                charB.DestroyCharacter();

                CharacterPositions[charA.PositionId] = string.Empty;
                CharacterPositions[cell.Id] = string.Empty;
                _draggableCharacters.Remove(cell.Id);
                _draggableCharacters.Remove(charA.PositionId);

                AddCharacter(newCharId, cell.Id);
            });
        }

        private bool CanMerge(int id, string characterId, out CharacterConfig nextConfig)
        {
            nextConfig = null;

            if (CharacterPositions[id].Equals(characterId))
            {
                nextConfig = _characterConfigCatalog.GetNext(_draggableCharacters[id].Config);
                return nextConfig != null;
            }

            return false;
        }

        private bool CanMerge(int id, string characterId) =>
            _characterConfigCatalog.GetNext(_draggableCharacters[id].Config) != null &&
            CharacterPositions[id].Equals(characterId);
    }

    public interface IFieldPresenter
    {
        void AddCharacter(string nameId, int cellId = -1);
    }
}