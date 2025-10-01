using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.App.Characters.Fabric;
using Game.Scripts.App.Characters.Menu;
using Game.Scripts.Menu.Effects.Particles;
using Game.Scripts.Menu.Field.CellScripts;
using Game.Scripts.Menu.MenuInput;
using Game.Scripts.Modules.Currency;
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
        private readonly CharacterMenuFactory _factory;
        private readonly CurrencyCell _moneyCell;
        private readonly GameSaveLoader _gameSaveLoader;
        private readonly ParticlesManager _particlesManager;
        private readonly IGameEventMediator _gameEventMediator;
        private readonly List<Cell> _cellViews = new List<Cell>();
        private readonly Dictionary<int, IDraggableCharacter> _draggableCharacters = new Dictionary<int, IDraggableCharacter>();
        private IDictionary<int, string> CharacterPositions => _field.CharacterPositions;
        
        private Cell _lastHoveredCell;
        private bool _isBoardFull;


        public FieldPresenter(FieldView fieldView,
            CellFactory cellFactory,
            IRaycaster raycaster,
            CharacterConfigCatalog characterConfigCatalog,
            Field field,
            CharacterMenuFactory factory,
            GameSaveLoader gameSaveLoader,
            CurrencyBank currencyBank,
            ParticlesManager particlesManager,
            IGameEventMediator gameEventMediator)
        {
            _fieldView = fieldView;
            _cellFactory = cellFactory;
            _raycaster = raycaster;
            _characterConfigCatalog = characterConfigCatalog;
            _field = field;
            _factory = factory;
            _gameSaveLoader = gameSaveLoader;
            _particlesManager = particlesManager;
            _gameEventMediator = gameEventMediator;
            _moneyCell = currencyBank.GetCell(CurrencyType.COIN);
        }

        public void Initialize()
        {
            _raycaster.Dropped += OnCharacterDropped;
            _raycaster.Hovered += OnCharacterHovered;
            _raycaster.HoverExited += OnHoverExited;
            _raycaster.OnTrash += OnCharacterSell;
            
            for (int i = 0; i < 16; i++)
            {
                Cell cell = _cellFactory.Create(_fieldView.Root, i);
                _cellViews.Add(cell);
            }

            CreateCharacters().Forget();
        }

        public void Dispose()
        {
            _raycaster.Dropped -= OnCharacterDropped;
            _raycaster.Hovered -= OnCharacterHovered;
            _raycaster.HoverExited -= OnHoverExited;
        }

        public bool IsBoardFull() => CharacterPositions.All(kvp => !string.IsNullOrEmpty(kvp.Value));
        public event Action OnStateChanged;

        public void AddCharacter(string nameId, int cellId = -1)
        {
            if (IsBoardFull())
            {
                Debug.LogWarning("Board is full");
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

            IDraggableCharacter draggable = _factory.Create(cell.transform.position, nameId);

            _particlesManager.PlayAppearCharacter(cell.transform.position);
            _gameEventMediator.NotifyCharacterMerged(draggable.Config);
            draggable.Appear();
            _draggableCharacters[cellId] = draggable;
            CharacterPositions[cellId] = nameId;
            draggable.PositionId = cellId;
            OnStateChanged?.Invoke();
        }

        private void OnCharacterSell(IDraggableCharacter character)
        {
            int price = _characterConfigCatalog.GetPrice(character.Config);

            _moneyCell.Add(price);
            
            CharacterPositions[character.PositionId] = string.Empty;
            _draggableCharacters.Remove(character.PositionId);
            
            character.DestroyCharacter();
        }

        private async UniTaskVoid CreateCharacters()
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
        event Action OnStateChanged;
        void AddCharacter(string nameId, int cellId = -1);
        bool IsBoardFull();
    }
}