using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;

namespace Game.Scripts.Menu.Characters
{
    public interface IDraggableCharacter
    {
        Transform Transform { get; }

        void OnPick();
        void OnDrag(Vector3 worldPos);
        void OnDrop(Cell targetCell);
    }
}