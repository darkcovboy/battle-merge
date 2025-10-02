using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI.CharacterInfo.Cards
{
    public class CardsOpenButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private CardsView _cardsView;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_button == null) _button = GetComponent<Button>();
        }
#endif
        
        [Inject]
        public void Construct(CardsView cardsView)
        {
            _cardsView = cardsView;
        }

        private void Start()
        {
            _button.onClick.AddListener(_cardsView.Open);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_cardsView.Open);
        }
    }
}