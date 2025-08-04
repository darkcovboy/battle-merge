using UnityEngine;

namespace Game.Scripts.Infrastructure.Loader.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}