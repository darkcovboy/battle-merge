using System;
using DG.Tweening;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Menu.CharacterUnlock;
using UnityEngine;
using Zenject;
using Game.Scripts.Useful.Extensions;

namespace Game.Scripts.Menu.UI.CharacterInfo
{
    public class AppearCharacterInfoView : MonoBehaviour
    {
        [SerializeField] private CharacterInfoProperties _characterInfoProperties;
        [SerializeField] private Transform _animatedObject;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _displayTime = 2f;

        private ICharacterUnlockService _characterUnlockService;

        [Inject]
        public void Construct(ICharacterUnlockService characterUnlockService)
        {
            _characterUnlockService = characterUnlockService;
            _characterUnlockService.CharacterUnlocked += OnCharacterUnlocked;
        }

        private void OnDestroy()
        {
            _characterUnlockService.CharacterUnlocked -= OnCharacterUnlocked;
        }

        private void OnCharacterUnlocked(CharacterConfig config)
        {
            _characterInfoProperties.Icon.sprite = config.Icon;
            _characterInfoProperties.DamageText.text = config.Damage.ToShortString();
            _characterInfoProperties.HealthText.text = config.Health.ToShortString();
            gameObject.SetActive(true);
            
            PlayAppearAnimation();
        }

        private void PlayAppearAnimation()
        {
            _animatedObject.DOKill();
        
            _animatedObject.localScale = Vector3.zero;
        
            Sequence sequence = DOTween.Sequence();
        
            sequence.Append(_animatedObject.DOScale(Vector3.one, _duration)
                .SetEase(Ease.OutBack));
        
            sequence.Append(_animatedObject.DOPunchScale(Vector3.one * 0.2f, 0.5f, 2));
        
            sequence.AppendInterval(_displayTime);
        
            sequence.Append(_animatedObject.DOScale(Vector3.zero, _duration * 0.7f)
                .SetEase(Ease.InBack));
        
            sequence.OnComplete(() => gameObject.SetActive(false));
        }
    }
}