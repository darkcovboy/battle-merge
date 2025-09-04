using UnityEngine;

namespace Game.Scripts.Menu.Field.CellScripts
{
    [CreateAssetMenu(fileName = "CellConfig", menuName = "Configs/UI/CellConfig")]
    public class CellConfig : ScriptableObject
    {
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _placeableColor = Color.green;
        [SerializeField] private Color _blockedColor = Color.orange;
        
        public Color DefaultColor => _defaultColor;
        public Color PlaceableColor => _placeableColor;
        public Color BlockedColor => _blockedColor;
    }
}