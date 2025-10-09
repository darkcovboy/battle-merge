using System;
using Game.Scripts.Infrastructure.Loader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI
{
    public class BattlerLoaderButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private SceneLoader _sceneLoader;
        [Inject]
        public void Cosntruct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _button.onClick.AddListener(()=> _sceneLoader.LoadSceneAsync("Gameplay"));
        }
    }
}