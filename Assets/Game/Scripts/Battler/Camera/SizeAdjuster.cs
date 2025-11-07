using Unity.Cinemachine;
using UnityEngine;

namespace Game.Scripts.Battler.Camera
{
    public class SizeAdjuster : MonoBehaviour
    {
        [SerializeField] private CinemachineGroupFraming _camera;
        [SerializeField] private float _wideScreenSize = 1.1f;
        [SerializeField] private float _mobileSize = 0.75f;
        [SerializeField] private float _wideAspectRatioThreshold = 1.6f;

        private Vector2 _lastScreenSize;

        private void Update()
        {
            if (Screen.width != (int)_lastScreenSize.x || Screen.height != (int)_lastScreenSize.y)
            {
                AdjustSize();
                _lastScreenSize = new Vector2(Screen.width, Screen.height);
            }
        }

        private void AdjustSize()
        {
            float aspectRatio = (float)Screen.width / Screen.height;
            _camera.FramingSize = aspectRatio >= _wideAspectRatioThreshold ? _wideScreenSize : _mobileSize;
        }
    }
}