using System;
using DG.Tweening;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;

namespace Game.Scripts.App.Characters.Menu
{
    public class DraggableCharacter : MonoBehaviour, IDraggableCharacter
    {
        public event Action OnPickCharacter;
        public event Action OnDropCharacter;
        public CharacterConfig Config { get; set; }
        public int PositionId { get; set; }

        public bool IsDragging { get; private set; }
        
        private Vector3 _lastPos;
        public Tweener Appear()
        {
            transform.localScale = Vector3.zero;
            return transform.DOScale(Vector3.one, 0.2f);
        }

        public Tweener Disappear()
        {
            transform.localScale = Vector3.one;
            return transform.DOScale(Vector3.zero, 0.2f);
        }

        public void DestroyCharacter()
        {
            DestroyImmediate(gameObject);
        }

        public void OnPick()
        {
            IsDragging = true;
            OnPickCharacter?.Invoke();
        }

        public void OnDrag(Vector3 worldPos)
        {
            transform.position = new Vector3(worldPos.x, 1.05f, worldPos.z);
        }

        public void OnDrop(Cell targetCell, bool withAnimation = false)
        {
            OnDropCharacter?.Invoke();
            IsDragging = false;
            

            if (withAnimation)
            {
                transform.DOMove(targetCell.transform.position, 0.2f);
            }
            else
            {
                if (targetCell != null)
                {
                    transform.position = targetCell.transform.position;
                }
            }
        }
    }
}