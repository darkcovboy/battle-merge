using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Menu.UI.Shop
{
    public class FloatingNotification : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _visibleDuration = 2f;
        [SerializeField] private float _moveYAmount = 50f;

        private Vector2 _startPosition;
        private Sequence _currentSequence;

        private void Awake()
        {
            _startPosition = _rectTransform.anchoredPosition;
            _canvasGroup.alpha = 0f;
        }

        [Button]
        public void ShowNotification()
        {
            _currentSequence?.Kill();
        
            _rectTransform.anchoredPosition = _startPosition;
        
            _currentSequence = DOTween.Sequence();
        
            _currentSequence.Append(_canvasGroup.DOFade(1f, _animationDuration));
            _currentSequence.Join(_rectTransform.DOAnchorPosY(_startPosition.y + _moveYAmount, _animationDuration));
        
            _currentSequence.AppendInterval(_visibleDuration);
        
            _currentSequence.Append(_canvasGroup.DOFade(0f, _animationDuration));
            _currentSequence.Join(_rectTransform.DOAnchorPosY(_startPosition.y + _moveYAmount * 2, _animationDuration));
        
            _currentSequence.OnComplete(() => {
                _rectTransform.anchoredPosition = _startPosition;
            });
        }
    }
}