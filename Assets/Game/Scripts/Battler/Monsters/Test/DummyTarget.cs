using UnityEngine;

namespace Game.Scripts.Battler.Monsters.Test
{
    public class DummyTarget : MonoBehaviour, IMonsterTarget
    {
        public float health = 100f;

        public Transform Transform => transform;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}