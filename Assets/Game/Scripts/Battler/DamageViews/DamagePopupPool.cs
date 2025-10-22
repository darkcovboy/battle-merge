using Game.Scripts.Battler.Module.ObjectPoolModule;
using UnityEngine;

namespace Game.Scripts.Battler.DamageViews
{
    public class DamagePopupPool :ObjectPool<DamagePopupView>
    {
        public DamagePopupPool(DamagePopupView prefab, int initialSize = 10, Transform parent = null) : base(prefab, initialSize, parent)
        {
        }
    }
}