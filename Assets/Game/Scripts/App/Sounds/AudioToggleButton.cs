using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.App.Sounds
{
    public enum AudioToggleType
    {
        Music,
        Sound
    }
    public class AudioToggleButton : MonoBehaviour
    {
        [SerializeField] private AudioToggleType _type;
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private ISoundManager _soundManager;

        [Inject]
        public void Construct(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
            UpdateIcon();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            switch (_type)
            {
                case AudioToggleType.Music:
                    _soundManager.ToggleMusic();
                    break;
                case AudioToggleType.Sound:
                    _soundManager.ToggleSound();
                    break;
            }

            UpdateIcon();
        }

        private void UpdateIcon()
        {
            bool isOn = _type switch
            {
                AudioToggleType.Music => _soundManager.IsMusicEnabled,
                AudioToggleType.Sound => _soundManager.IsSoundEnabled,
                _ => true
            };

            _icon.sprite = isOn ? _onSprite : _offSprite;
        }
    }
}