using System.Collections;
using UnityEngine;

namespace Game.Scripts.Battler.FortniteLikeZone
{
    public class BattleZoneScaler : MonoBehaviour
    {
        [Header("Scale Settings")]
        [SerializeField] private float maxScale = 20f;
        [SerializeField] private float minScale = 5f;
        [SerializeField] private float duration = 30f; 

        [Header("Runtime State")]
        [SerializeField] private bool isShrinking = false;
        [SerializeField] private bool isPaused = false;

        private Coroutine _scalingRoutine;
        private float _elapsedTime;
        private void OnEnable()
        {
            transform.localScale = new Vector3(maxScale, transform.localScale.y, maxScale);
            _elapsedTime = 0f;
            StartShrinking();
        }

        public void PauseScaling(bool pause)
        {
            isPaused = pause;
        }

        private void StartShrinking()
        {
            if (_scalingRoutine != null)
                StopCoroutine(_scalingRoutine);

            isShrinking = true;
            _scalingRoutine = StartCoroutine(ShrinkRoutine());
        }

        private IEnumerator ShrinkRoutine()
        {
            Vector3 startScale = new Vector3(maxScale, transform.localScale.y, maxScale);
            Vector3 endScale = new Vector3(minScale, transform.localScale.y, minScale);

            _elapsedTime = 0f;

            while (_elapsedTime < duration)
            {
                if (!isPaused)
                {
                    _elapsedTime += Time.deltaTime;
                    float t = _elapsedTime / duration;
                    transform.localScale = Vector3.Lerp(startScale, endScale, t);
                }

                yield return null;
            }

            transform.localScale = endScale;
            isShrinking = false;
            _scalingRoutine = null;
        }
    }
}