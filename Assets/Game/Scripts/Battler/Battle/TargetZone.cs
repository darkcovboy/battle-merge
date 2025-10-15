using Game.Scripts.Battler.Player;
using UnityEngine;

namespace Game.Scripts.Battler.Battle
{
    public class TargetZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCharacter player))
            {
                Debug.Log("Battle started!");
                player.StartBattle();
            }
        }
    }
}