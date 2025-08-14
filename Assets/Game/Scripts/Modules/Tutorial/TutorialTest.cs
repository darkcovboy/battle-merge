using System;
using Game.Scripts.Modules.SaveLoad;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Modules.Tutorial
{
    public class TutorialTest : MonoBehaviour
    {
        private TutorialService _tutorial;
        private GameSaveLoader _gameSaveLoader;

        [Inject]
        public void Constructor(TutorialService tutorialService, GameSaveLoader gameSaveLoader)
        {
            _tutorial = tutorialService;
            _gameSaveLoader = gameSaveLoader;
        }

        private void Start()
        {
            Debug.Log("Tutorial completed " + _tutorial.IsCompleted);
        }

        [Button]
        public void SetCompleteTutorial()
        {
            _tutorial.MarkCompleted();
            _gameSaveLoader.Save();
        }
    }
}