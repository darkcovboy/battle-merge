using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Menu.UI.Shop
{
    public class SkinService
    {
        public int CurrentSkin { get; set; }
        public List<int> UnlockedSkins { get; set; }

        public void Setup(int currentSkinID, List<int> unlockedSkinIDs)
        {
            CurrentSkin = currentSkinID;
            UnlockedSkins = unlockedSkinIDs;
        }
    }
}