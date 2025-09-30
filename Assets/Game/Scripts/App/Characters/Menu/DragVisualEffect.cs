using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.App.Characters.Menu
{
    public class DragVisualEffect : MonoBehaviour
    {
        [SerializeField] private DraggableCharacter _draggableCharacter;
        [SerializeField] private DragEffectType _effectType = DragEffectType.SquashAndStretch;
        [SerializeField] private float _intensity = 0.15f; // универсальный параметр
        [SerializeField] private float _duration = 0.2f;
        
        private Vector3 _defaultScale;
        private Quaternion _defaultRotation;
        private Tween _activeTween;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(_draggableCharacter == null)
                _draggableCharacter = GetComponent<DraggableCharacter>();
        }
#endif

        private void Awake()
        {
            _defaultScale = transform.localScale;
            _defaultRotation = transform.localRotation;
        }

        private void OnEnable()
        {
            _draggableCharacter.OnPickCharacter += OnPick;
            _draggableCharacter.OnDropCharacter += OnDrop;
        }

        private void OnDisable()
        {
            _draggableCharacter.OnPickCharacter -= OnPick;
            _draggableCharacter.OnDropCharacter -= OnDrop;
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

    