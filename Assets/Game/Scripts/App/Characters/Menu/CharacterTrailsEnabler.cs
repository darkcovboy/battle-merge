using System;
using System.Collections.Generic;
using Game.Scripts.Menu.Effects.Trails;
using Unity.Collections;
using UnityEngine;

namespace Game.Scripts.App.Characters.Menu
{
    public class CharacterTrailsEnabler : MonoBehaviour
    {
        [SerializeField] private DraggableCharacter _draggableCharacter;
        [SerializeField,ReadOnly] private List<TrailCharacter> _trailCharacter;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(_draggableCharacter == null)
                _draggableCharacter = GetComponent<DraggableCharacter>();
            
            _trailCharacter = new List<TrailCharacter>();
            var trailComponents = GetComponentsInChildren<TrailCharacter>(true);
            _trailCharacter.AddRange(trailComponents);
        }
#endif

        private void Awake()
        {
            DisableTrails();
        }

        private void OnEnable()
        {
            _draggableCharacter.OnPickCharacter += EnableTrails;
            _draggableCharacter.OnDropCharacter += DisableTrails;
        }
        
        private void OnDisable()
        {
            _draggableCharacter.OnPickCharacter -= EnableTrails;
            _draggableCharacter.OnDropCharacter -= DisableTrails;
        }

        private void EnableTrails()
        {
            foreach (var trailCharacter in _trailCharacter)
            {
                trailCharacter.gameObject.SetActive(true);
            }
        }

        private void DisableTrails()
        {
            foreach (var trailCharacter in _trailCharacter)
            {
                trailCharacter.gameObject.SetActive(false);
            }
        }
    }
}