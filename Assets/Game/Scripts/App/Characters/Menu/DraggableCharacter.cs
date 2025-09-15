using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;

namespace Game.Scripts.Menu.Characters
{
    public class DraggableCharacter : MonoBehaviour, IDraggableCharacter
    {
        public string Id { get; set; }
        public int PositionId { get; set; }

        public bool IsDragging { get; private set; }

        public void OnPick()
        {
            IsDragging = true;
            Debug.Log("Picked character");
        }

        public void OnDrag(Vector3 worldPos)
        {
            transform.position = new Vector3(worldPos.x, 1.05f, worldPos.z);
        }

        public void OnDrop(Cell targetCell)
        {
            IsDragging = false;
            if (targetCell != null)
            {
                transform.position = targetCell.transform.position + Vector3.up * 0.5f;
            }
            else
            {
                Debug.Log("Dropped outside any cell");
            }
        }
    }
}