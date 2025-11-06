using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Transform _cardsParent;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public event Action OnSelectClicked;
        public event Action OnBuyClicked;
        public event Action OnBackClicked;
        
        public Transform CardsParent => _cardsParent;
        
        private Sequence _showSequence;
        private Sequence _hideSequence;
        
        
        private void Awake()
        {
            _buyButton.onClick.AddListener(() => OnBuyClicked?.Invoke());
            _selectButton.onClick.AddListener(() => OnSelectClicked?.Invoke());
            _backButton.onClick.AddListener(() => OnBackClicked?.Invoke());
        }

        public void ShowBuyButton()
        {
            _buyButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
        }

        public void ShowSelectButton()
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }

        public void HideAllActionButtons()
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            transform.localScale = Vector3.one * 0.9f;

            _showSequence?.Kill();
            _showSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, 0.25f))
                .Join(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack));

        }

        public void Hide()
        {
            _hideSequence?.Kill();
            _hideSequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(0f, 0.2f))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });

        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
            _selectButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        public void SetBuyButtonState(bool exists)
        {
            _buyButton.interactable = exists;
        }
    }
}