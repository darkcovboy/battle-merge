using Game.Scripts.Useful.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Menu.UI.CharacterInfo.Cards
{
    public class CardCellView : MonoBehaviour
    {
        [SerializeField] private CharacterInfoProperties _characterInfoProperties;
        [SerializeField] private Image _lockImage;

        public string NameId { get; private set; }

        public void Setup(string configNameId, Sprite icon, float damage, float health, bool isUnlock)
        {
            NameId = configNameId;
            _characterInfoProperties.Icon.sprite = icon;
            _characterInfoProperties.DamageText.text = damage.ToShortString();
            _characterInfoProperties.HealthText.text = health.ToShortString();
            _lockImage.gameObject.SetActive(!isUnlock);
        }

        public void Unlock()
        {
            _lockImage.gameObject.SetActive(false);
        }
    }
}