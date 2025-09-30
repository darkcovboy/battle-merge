using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.App.Characters.Menu
{
    public class OutlineHighlighter : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private DraggableCharacter _draggableCharacter;

        [SerializeField] private Color _highlightColor = Color.yellow;
        [SerializeField] private float _transitionSpeed = 5f;

        private MaterialPropertyBlock _mpb;
        private Color _defaultColor;
        private float _defaultWidth;
        private Coroutine _transitionRoutine;

        private void Awake()
        {
            if (_renderer == null)
                _renderer = GetComponentInChildren<SkinnedMeshRenderer>();

            _mpb = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_mpb);

            _defaultColor = _mpb.GetColor("_OutlineColor");
            _defaultWidth = _mpb.GetFloat("_OutlineWidth");
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_draggableCharacter == null)
                _draggableCharacter = GetComponent<DraggableCharacter>();
            
            if (_renderer == null)
                _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
#endif

        private void OnEnable()
        {
            _draggableCharacter.OnPickCharacter += EnableHighlight;
            _draggableCharacter.OnDropCharacter += DisableHighlight;
        }

        private void OnDisable()
        {
            _draggableCharacter.OnPickCharacter -= EnableHighlight;
            _draggableCharacter.OnDropCharacter -= DisableHighlight;
        }

        public void EnableHighlight()
        {
            StartTransition(_highlightColor);
        }

        public void DisableHighlight()
        {
            StartTransition(_defaultColor);
        }

        private void StartTransition(Color targetColor)
        {
            if (_transitionRoutine != null)
                StopCoroutine(_transitionRoutine);

            _transitionRoutine = StartCoroutine(TransitionRoutine(targetColor));
        }

        private IEnumerator TransitionRoutine(Color targetColor)
        {
            _renderer.GetPropertyBlock(_mpb);
            Color startColor = _mpb.GetColor("_OutlineColor");

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * _transitionSpeed;

                Color color = Color.Lerp(startColor, targetColor, t);

                _mpb.SetColor("_OutlineColor", color);
                _renderer.SetPropertyBlock(_mpb);

                yield return null;
            }

            _mpb.SetColor("_OutlineColor", targetColor);
            _renderer.SetPropertyBlock(_mpb);

            _transitionRoutine = null;
        }
    }
}