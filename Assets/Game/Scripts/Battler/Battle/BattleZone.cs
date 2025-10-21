using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Battler.Battle
{
    [RequireComponent(typeof(Collider))]
    public class BattleZone : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _ownerCharacter;
        
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _height = 2f;

        public ICombat Owner { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_ownerCharacter is ICombat) return;
            Debug.LogError($"{nameof(_ownerCharacter)} is not a Combat");
            _ownerCharacter = null;
        }
#endif

        private void Awake()
        {
            if (_ownerCharacter is ICombat combatant)
                Owner = combatant;
            else
                Debug.LogError($"{_ownerCharacter.name} does not implement ICombatant!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BattleZone otherZone) && otherZone.Owner != null && otherZone.Owner != Owner)
            {
                BattleController.Instance.StartBattle(Owner, otherZone.Owner);
            }
        }

        public Vector3 GetRandomPointInside()
        {
            Vector2 circle = Random.insideUnitCircle * _radius;
            return transform.position + new Vector3(circle.x, 0, circle.y);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
    }
}