using System;
using DG.Tweening;
using Game.Scripts.App.Characters.Menu;
using Game.Scripts.Menu.Field.CellScripts;
using Game.Scripts.Menu.MenuInput;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI.CharacterInfo
{
    public class CharacterInfoView : MonoBehaviour
    {
        [SerializeField] private CharacterInfoProperties _characterInfoProperties;
        
        [Header("Animation Settings")] 
        [SerializeField] private float _animationDuration = 0.3f;
        [SerializeField] private Ease _appearEase = Ease.OutBack;
        [SerializeField] private Ease _disappearEase = Ease.InBack;


        private IRaycaster _raycaster;
        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private Vector2 _hiddenPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;
            
            _hiddenPosition = _originalPosition;
            _hiddenPosition.x = -Screen.width +_rectTransform.rect.width;
            _rectTransform.anchoredPosition = _hiddenPosition;
        }

        [Inject]
        public void Construct(IRaycaster raycaster)
        {
            _raycaster = raycaster;
            _raycaster.OnPick += Appear;
            _raycaster.Dropped += Disappear;
            _raycaster.OnTrash += Disappear;
        }

        private void OnDestroy()
        {
            if (_raycaster != null)
            {
                _raycaster.OnPick -= Appear;
                _raycaster.Dropped -= Disappear;
                _raycaster.OnTrash -= Disappear;
            }
            
            _rectTransform.DOKill();
        }

        private void Appear(IDraggableCharacter draggableCharacter)
        {
            _rectTransform.DOKill();

            _characterInfoProperties.Icon.sprite = draggableCharacter.Config.Icon;
            _characterInfoProperties.DamageText.text = draggableCharacter.Config.Damage.ToString();
            _characterInfoProperties.HealthText.text = draggableCharacter.Config.Health.ToString();

            _rectTransform.DOAnchorPos(_originalPosition, _animationDuration)
                .SetEase(_appearEase);
        }

        private void Disappear(Cell cell, IDraggableCharacter draggableCharacter)
        {
            Hide();
        }
        private void Disappear(IDraggableCharacter draggableCharacter)
        {
            Hide();
        }

        private void Hide()
        {
            _rectTransform.DOKill();

            Vector2 targetPosition = _originalPosition;
            targetPosition.x = Screen.width + _rectTransform.rect.width;
            
            _rectTransform.DOAnchorPos(targetPosition, _animationDuration)
                .SetEase(_disappearEase)
                .OnComplete(()=>_rectTransform.anchoredPosition = _hiddenPosition);
        }
    }

    [Serializable]
    public class CharacterInfoProperties
    {
        [field: SerializeField] public Image Icon { get; private set; }
        [field:SerializeField] public TextMeshProUGUI DamageText{ get; private set; }
        [field:SerializeField] public TextMeshProUGUI HealthText{ get; private set; }

    }
}