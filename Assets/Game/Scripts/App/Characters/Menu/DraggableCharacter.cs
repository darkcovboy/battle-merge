using DG.Tweening;
using Game.Scripts.Menu.Characters;
using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;

namespace Game.Scripts.App.Characters.Menu
{
    public class DraggableCharacter : MonoBehaviour, IDraggableCharacter
    {
        public string Id { get; set; }
        public int PositionId { get; set; }

        public bool IsDragging { get; private set; }
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
        }

        public void OnDrag(Vector3 worldPos)
        {
            transform.position = new Vector3(worldPos.x, 1.05f, worldPos.z);
        }

        public void OnDrop(Cell targetCell, bool withAnimation = false)
        {
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