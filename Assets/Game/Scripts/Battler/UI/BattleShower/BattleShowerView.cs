using DG.Tweening;
using Game.Scripts.Useful.Extensions;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Battler.UI.BattleShower
{
    public class BattleShowerView : MonoBehaviour
    {
        [Header("UI Elements")] 
        [SerializeField] private RectTransform _player;
        [SerializeField] private RectTransform _opponent;
        [SerializeField] private RectTransform _vsImage;
        
        [Header("Player Stats")]
        [SerializeField] private TextMeshProUGUI _playerHpText;
        [SerializeField] private TextMeshProUGUI _playerAtkText;

        [Header("Enemy Stats")]
        [SerializeField] private TextMeshProUGUI _enemyHpText;
        [SerializeField] private TextMeshProUGUI _enemyAtkText;

        [Header("Root")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [Header("Animation Settings")]
        [SerializeField] private float _enterDuration = 0.6f;
        [SerializeField] private float _exitDuration = 0.4f;
        [SerializeField] private float _vsDropDistance = 300f;
        [SerializeField] private float _sideOffset = 600f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        
        private Vector2 _playerStartPos;
        private Vector2 _opponentStartPos;
        private Vector2 _vsStartPos;
        
        private void Awake()
        {
            _playerStartPos = _player.anchoredPosition;
            _opponentStartPos = _opponent.anchoredPosition;
            _vsStartPos = _vsImage.anchoredPosition;
        }
        
        public void UpdatePlayerStats(float health, float attack)
        {
            _playerHpText.text = $"{health.ToShortString()}";
            _playerAtkText.text = $"{attack.ToShortString()}";
        }

        public void UpdateEnemyStats(float health, float attack)
        {
            _enemyHpText.text = $"{health.ToShortString()}";
            _enemyAtkText.text = $"{attack.ToShortString()}";
        }

        public void AnimateShow()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(1, 0.3f);

            _player.anchoredPosition = _playerStartPos - new Vector2(_sideOffset, 0);
            _opponent.anchoredPosition = _opponentStartPos + new Vector2(_sideOffset, 0);
            _vsImage.anchoredPosition = _vsStartPos + new Vector2(0, _vsDropDistance);

            _player.DOAnchorPos(_playerStartPos, _enterDuration).SetEase(_ease);
            _opponent.DOAnchorPos(_opponentStartPos, _enterDuration).SetEase(_ease);
            _vsImage.DOAnchorPos(_vsStartPos, _enterDuration * 0.9f).SetEase(Ease.OutBounce);
        }

        public void AnimateHide(System.Action onComplete = null)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(_canvasGroup.DOFade(0, _exitDuration * 0.8f));
            seq.Join(_player.DOAnchorPos(_playerStartPos - new Vector2(_sideOffset, 0), _exitDuration).SetEase(Ease.InBack));
            seq.Join(_opponent.DOAnchorPos(_opponentStartPos + new Vector2(_sideOffset, 0), _exitDuration).SetEase(Ease.InBack));
            seq.Join(_vsImage.DOAnchorPos(_vsStartPos + new Vector2(0, _vsDropDistance), _exitDuration * 0.8f).SetEase(Ease.InBack));

            seq.OnComplete(() =>
            {
                _canvasGroup.alpha = 0;
                onComplete?.Invoke();
            });
        }
    }
}