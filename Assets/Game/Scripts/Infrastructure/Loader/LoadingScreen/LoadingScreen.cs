using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Infrastructure.Loader.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image _background;
        [SerializeField] private Slider _progressBarFill;

        [Header("Background Animation")]
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private float _colorChangeSpeed = 1f;

        private float _gradientTime;
        
        private void Update()
        {
            AnimateBackground();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);

            if (_progressBarFill != null)
                _progressBarFill.value = progress;
        }
        
        private void AnimateBackground()
        {
            if (_background == null || _colorGradient == null) return;

            _gradientTime += Time.deltaTime * _colorChangeSpeed;
            float t = Mathf.PingPong(_gradientTime, 1f);
            _background.color = _colorGradient.Evaluate(t);
        }
    }
}