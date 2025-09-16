using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.Field.CellScripts
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public int Id { get; private set; }

        private CellConfig _config;

        public void Setup(CellConfig config, int id)
        {
            Id = id;
            _config = config;
            _text.text = Id.ToString();
        }

        public void SetDefault()   { if (_image) _image.color = _config.DefaultColor; }
        public void SetPlaceable() { if (_image) _image.color = _config.PlaceableColor; }
        public void SetBlocked()   { if (_image) _image.color = _config.BlockedColor; }

    }
}