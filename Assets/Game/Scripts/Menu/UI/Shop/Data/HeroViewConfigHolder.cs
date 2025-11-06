using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Menu.UI.Shop.Data
{
    [CreateAssetMenu(menuName = "Configs/Shop/ShopConfigHolder")]
    public class HeroViewConfigHolder : ScriptableObject
    {
        public List<HeroViewConfig> Skins;

        public HeroViewConfig GetById(int id) =>
            Skins.Find(s => s.Id == id);
    }
}