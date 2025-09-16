using System;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Menu.Field;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.BuyButtons
{
    public class BuyButtonCharacterPresenter : IInitializable, IDisposable
    {
        private readonly BuyButtonCharacterView[] _views;
        private readonly IFieldPresenter _fieldPresenter;

        public BuyButtonCharacterPresenter(BuyButtonCharacterView[] views, IFieldPresenter fieldPresenter)
        {
            _views = views;
            _fieldPresenter = fieldPresenter;
        }
        
        public void Initialize()
        {
            foreach (var view in _views)
            {
                view.OnClick += OnButtonClicked;
            }
        }

        public void Dispose()
        {
            foreach (var view in _views)
            {
                view.OnClick -= OnButtonClicked;
            }
        }

        private void OnButtonClicked(CharacterWarriorType characterWarriorType)
        {
            Debug.Log("OnButtonClicked");
            switch (characterWarriorType)
            {
                case CharacterWarriorType.Fighter:
                    _fieldPresenter.AddCharacter("fighter_1");
                    break;
                case CharacterWarriorType.Shooter:
                    _fieldPresenter.AddCharacter("shooter_1");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}