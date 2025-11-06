using System;
using Game.Scripts.Menu.UI.Shop.ModelsDisplay;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Menu.UI.Shop.Data
{
    [Serializable]
    public class HeroViewConfig
    {
        public int Id;
        [ShowInInspector, PreviewField(60), LabelText("Icon")]
        public Sprite Icon;
        public int Price;
        public SkinModelView ModelPrefab;
    }
}