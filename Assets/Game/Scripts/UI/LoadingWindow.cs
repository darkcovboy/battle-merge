using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private Slider _progressBar;

        public void Show(string status)
        {
            _root.SetActive(true);
            _statusText.text = status;
            _progressBar.value = 0f;
        }

        public void SetProgress(float progress)
        {
            _progressBar.value = Mathf.Clamp01(progress);
        }

        public void Hide()
        {
            _root.SetActive(false);
        }

    }
}