using System.Collections.Generic;

namespace Game.Scripts.Menu.Field
{
    public class Field
    {
        public Dictionary<int,string> CharacterPositions { get; set; }
        
        public void Setup(Dictionary<int, string> dataItems)
        {
            CharacterPositions = dataItems;
        }
    }
}