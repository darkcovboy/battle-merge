using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;

namespace Game.Scripts.Menu.Characters
{
    public interface IDraggableCharacter
    {
        string Id { get; set; }
        int PositionId { get; set; }
        bool IsDragging { get; }
        void OnPick();
        void OnDrag(Vector3 worldPos);
        void OnDrop(Cell targetCell);
    }
}