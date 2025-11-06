using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI.Shop
{
    public class HeroCardView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _gemIcon;
        [SerializeField] private GameObject _ownedText;
        [SerializeField] private GameObject _lockOverlay;
        [SerializeField] private Button _button;

        private int _id;
        public int Id => _id;
        public event Action<int> OnClicked;

        public void Setup(int id, Sprite icon, int price)
        {
            _id = id;
            _icon.sprite = icon;
            _priceText.text = price.ToString();
            _button.onClick.AddListener(() => OnClicked?.Invoke(_id));
        }
        
        public void SetUnlocked(bool isUnlocked)
        {
            _lockOverlay.SetActive(!isUnlocked);
            _gemIcon.SetActive(!isUnlocked);
            _priceText.gameObject.SetActive(!isUnlocked);
            _ownedText.SetActive(isUnlocked);
        }
        

        private void OnDestroy() =>
            _button.onClick.RemoveAllListeners();
    }
}