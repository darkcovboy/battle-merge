using Game.Scripts.Battler.Module.ObjectPoolModule;
using UnityEngine;

namespace Game.Scripts.Battler.Player.Pokeballs
{
    public class PokeballPool : ObjectPool<Pokeball>
    {
        public PokeballPool(Pokeball prefab, int initialSize = 10, Transform parent = null) : base(prefab, initialSize, parent)
        {
        }
    }
}