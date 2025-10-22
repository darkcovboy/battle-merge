using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Battler.Camera;
using Game.Scripts.Battler.UI.BattleShower;
using UnityEngine;

namespace Game.Scripts.Battler.Battle
{
    public class BattleController
    {
        private readonly BattlerShowerPresenter _presenter;
        private readonly List<Battle> _activeBattles = new();

        public BattleController(BattlerShowerPresenter presenter)
        {
            _presenter = presenter;
        }

        public void StartBattle(ICombat a, ICombat b)
        {
            if (_activeBattles.Any(btl => btl.Involves(a) || btl.Involves(b)))
                return;

            var battle = new Battle(a, b);
            _activeBattles.Add(battle);

            battle.OnBattleEnded += HandleBattleEnded;
            battle.Start();
            
            if (a.IsPlayer || b.IsPlayer)
            {
                var player = a.IsPlayer ? a : b;
                var enemy = a.IsPlayer ? b : a;
                _presenter.Show(player, enemy);
                CameraFightController.Instance.StartFight(a.IsPlayer ? b : a);
            }
        }

        private void HandleBattleEnded(Battle battle)
        {
            _activeBattles.Remove(battle);
        }
    }
}