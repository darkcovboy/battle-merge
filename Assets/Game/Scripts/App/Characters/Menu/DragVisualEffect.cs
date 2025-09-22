using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.App.Characters.Menu
{
    public class DragVisualEffect : MonoBehaviour
    {
        [SerializeField] private DragEffectType _effectType = DragEffectType.SquashAndStretch;
        [SerializeField] private float _intensity = 0.15f; // универсальный параметр
        [SerializeField] private float _duration = 0.2f;
        
        private Vector3 _defaultScale;
        private Quaternion _defaultRotation;
        private Tween _activeTween;

        private void Awake()
        {
            _defaultScale = transform.localScale;
            _defaultRotation = transform.localRotation;
        }

        public void OnPick()
        {
            switch (_effectType)
            {
                case DragEffectType.SquashAndStretch:
                    _activeTween?.Kill();
                    _activeTween = transform.DOScale(
                        new Vector3(_defaultScale.x + _intensity, _defaultScale.y - _intensity, _defaultScale.z + _intensity),
                        _duration
                    ).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                    break;

                case DragEffectType.Tilt:
                    _activeTween?.Kill();
                    _activeTween = transform.DORotate(new Vector3(0, 0, 10f), _duration)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine);
                    break;

                case DragEffectType.Bounce:
                    _activeTween?.Kill();
                    _activeTween = transform
                        .DOJump(transform.position, _intensity, 1, _duration)
                        .SetLoops(-1, LoopType.Yoyo);
                    break;
            }
        }

        public void OnDrop()
        {
            _activeTween?.Kill();

            transform.DOScale(_defaultScale, 0.2f);
            transform.DORotateQuaternion(_defaultRotation, 0.2f);
        }


        private enum DragEffectType
        {
            None,
            SquashAndStretch,
            Tilt,
            Bounce
        }
    }
}

    