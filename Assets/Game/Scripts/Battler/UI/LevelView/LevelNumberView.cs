using System;
using Game.Scripts.App.GameScenes;
using Game.Scripts.App.Localisation;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.UI.LevelView
{
    public class LevelNumberView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private GameSceneManager _gameSceneManager;
        private LocalisationManager _localisationManager;

        [Inject]
        private void Construct(GameSceneManager gameSceneManager, LocalisationManager localisationManager)
        {
            _gameSceneManager = gameSceneManager;
            _localisationManager = localisationManager;
        }

        private void Start()
        {
            _text.text = _localisationManager.GetText("level",_gameSceneManager.CurrentUILevel);
        }
    }
}