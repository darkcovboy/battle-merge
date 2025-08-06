using UnityEngine;

namespace Game.Scripts.Menu.Field
{
    public class FieldView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        
        public Transform Root => _root;
    }
}