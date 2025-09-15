using System;
using Game.Scripts.Menu.Characters;
using Game.Scripts.Menu.Field.CellScripts;

namespace Game.Scripts.Menu.MenuInput
{
    public interface IRaycaster
    {
        event Action<Cell, IDraggableCharacter> Dropped;
    }
}