using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Battler.Camera;
using UnityEngine;

namespace Game.Scripts.Battler.Battle
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance { get; private set; }

        private readonly List<Battle> _activeBattles = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void StartBattle(ICombat a, ICombat b)
        {
            if (_activeBattles.Any(btl => btl.Involves(a) || btl.Involves(b)))
                return;

            var battle = new Battle(a, b);
            _activeBattles.Add(battle);

            battle.OnBattleEnded += HandleBattleEnded;
            battle.Start();

            CameraFightController.Instance.StartFight(a.IsPlayer ? b : a);
        }

        private void HandleBattleEnded(Battle battle)
        {
            _activeBattles.Remove(battle);
        }
    }
}