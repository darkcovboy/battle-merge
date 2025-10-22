using DG.Tweening;
using Game.Scripts.Useful.Extensions;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Battler.DamageViews
{
    public class DamagePopupView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _riseHeight = 1.5f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Ease _ease = Ease.OutQuad;

        private UnityEngine.Camera _camera;
        private Vector3 _startPos;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (_camera != null)
                transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }

        public void Play(float damage, Color color, System.Action onComplete = null)
        {
            _text.text = damage.ToShortString();
            _text.color = color;

            _startPos = transform.position;
            transform.localScale = Vector3.zero;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack));
            seq.Join(transform.DOMoveY(_startPos.y + _riseHeight, _duration).SetEase(_ease));
            seq.Join(_text.DOFade(0, _duration).SetEase(Ease.InQuad).SetDelay(0.3f));
            seq.OnComplete(() =>
            {
                _text.alpha = 1f;
                transform.localScale = Vector3.one;
                onComplete?.Invoke();
            });
        }

    }
}