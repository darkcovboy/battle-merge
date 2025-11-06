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
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _button.onClick.AddListener(()=> _sceneLoader.LoadSceneAsync("Gameplay").Forget());
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}