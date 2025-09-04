using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.Field.CellScripts
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public int ID { get; private set; }

        private CellConfig _config;

        public void Setup(CellConfig config, int id)
        {
            ID = id;
            _config = config;
        }
        
        public void SetDefault()   { if (_image) _image.color = _config.DefaultColor; }
        public void SetPlaceable() { if (_image) _image.color = _config.PlaceableColor; }
        public void SetBlocked()   { if (_image) _image.color = _config.BlockedColor; }

    }
}