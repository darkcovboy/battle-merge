using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Menu.Field
{
    public class Field
    {
        public Dictionary<int,string> CharacterPositions { get; set; }
        
        public void Setup(Dictionary<int, string> dataItems)
        {
            Debug.Log("Setup");
            CharacterPositions = dataItems;
        }
    }
}