using UnityEngine;

namespace Game.Scripts.Menu.UI.Shop.ModelsDisplay
{
    public class SkinPreviewRotator : MonoBehaviour
    {
        [SerializeField] private float _speed = 30f;
        private void Update() => transform.Rotate(Vector3.up, _speed * Time.deltaTime);
    }
}