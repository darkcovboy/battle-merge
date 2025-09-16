using System;
using Game.Scripts.App.Characters.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.Menu.BuyButtons
{
    public class BuyButtonCharacterView : MonoBehaviour
    {
        public event Action<CharacterWarriorType> OnClick;
        
        [SerializeField]
        private Button _button;
        
        [SerializeField] 
        private CharacterWarriorType _characterWarriorType;

        private void Awake()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            OnClick?.Invoke(_characterWarriorType);
        }
    }
}