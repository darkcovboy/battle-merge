using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI.CharacterInfo.Cards
{
    public class CardsView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openFighters;
        [SerializeField] private Button _openShooters;
        [SerializeField] private Transform _fighters;
        [SerializeField] private Transform _shooters;
        [SerializeField] private Transform _cardsFightersContainer;
        [SerializeField] private Transform _cardsShootersContainer;
        
        [Header("Animation Settings")]
        [SerializeField] private float _animationDuration = 0.5f;

        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private Vector2 _hiddenPosition;


        public Transform CardsFightersContainer => _cardsFightersContainer;
        public Transform CardsShootersContainer => _cardsShootersContainer;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            CalculatePositions();

            _closeButton.onClick.AddListener(Close);
            _openFighters.onClick.AddListener(OpenFighters);
            _openShooters.onClick.AddListener(OpenShooters);
            
            _rectTransform.anchoredPosition = _hiddenPosition;
        }

        private void Start()
        {
            OpenFighters();
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
            _openFighters.onClick.RemoveListener(OpenFighters);
            _openShooters.onClick.RemoveListener(OpenShooters);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            
            _rectTransform.DOAnchorPos(_originalPosition, _animationDuration)
                .OnComplete(() => {
                    // Дополнительные действия после завершения анимации
                    Debug.Log("Menu opened!");
                });
        }

        private void CalculatePositions()
        {
            _originalPosition = _rectTransform.anchoredPosition;
        
            float screenHeight = Screen.height;
            float panelHeight = _rectTransform.rect.height;
            _hiddenPosition = new Vector2(_originalPosition.x, -screenHeight - panelHeight);
        }

        private void OpenFighters()
        {
            _fighters.gameObject.SetActive(true);
            _shooters.gameObject.SetActive(false);
        }

        private void OpenShooters()
        {
            _fighters.gameObject.SetActive(false);
            _shooters.gameObject.SetActive(true);
        }

        private void Close()
        {
            _rectTransform.DOAnchorPos(_hiddenPosition, _animationDuration)
                .OnComplete(() => {
                    gameObject.SetActive(false);
                    Debug.Log("Menu closed!");
                });
        }
    }
}