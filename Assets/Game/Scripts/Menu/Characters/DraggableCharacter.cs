using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;

namespace Game.Scripts.Menu.Characters
{
    public class DraggableCharacter : MonoBehaviour, IDraggableCharacter
    {
        public Transform Transform => transform;

        public void OnPick()
        {
            Debug.Log("Picked character");
        }

        public void OnDrag(Vector3 worldPos)
        {
            // Raycaster сам считает worldPos (например пересечение с плоскостью)
            transform.position = new Vector3(worldPos.x, 0.5f, worldPos.z);
        }

        public void OnDrop(Cell targetCell)
        {
            if (targetCell != null)
            {
                transform.position = targetCell.transform.position + Vector3.up * 0.5f;
                Debug.Log("Dropped on cell: " + targetCell.name);
            }
            else
            {
                Debug.Log("Dropped outside any cell");
            }
        }
    }
}