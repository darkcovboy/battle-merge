using UnityEngine;

namespace Game.Scripts.Battler.Battle
{
    [RequireComponent(typeof(Collider))]
    public class BattleZone : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _height = 2f;

        public Vector3 GetRandomPointInside()
        {
            Vector2 circle = Random.insideUnitCircle * _radius;
            return transform.position + new Vector3(circle.x, 0, circle.y);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
    }
}