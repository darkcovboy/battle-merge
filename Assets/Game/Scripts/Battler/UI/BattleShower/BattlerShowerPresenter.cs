using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Battler.Battle;

namespace Game.Scripts.Battler.UI.BattleShower
{
    public class BattlerShowerPresenter
    {
        private const float Delay = 0.5f;
        private readonly BattleShowerView _view;
        

        private CancellationTokenSource _cts;
        private ICombat _player;
        private ICombat _opponent;

        public BattlerShowerPresenter(BattleShowerView view)
        {
            _view = view;
        }

        public void Show(ICombat player, ICombat opponent)
        {
            _player = player;
            _opponent = opponent;
            _view.AnimateShow();
            
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            StartUpdateLoopAsync(_cts.Token).Forget();

        }
        
        public void Hide()
        {
            _cts?.Cancel();
            _cts = null;
            _view.AnimateHide();
        }

        private async UniTaskVoid StartUpdateLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                UpdateStats();
                await UniTask.Delay((int)(Delay * 1000), cancellationToken: token);
            }
        }

        private void UpdateStats()
        {
            if (_player == null || _opponent == null)
                return;

            _view.UpdatePlayerStats(_player.TeamController.GetTotalHealth(), _player.TeamController.GetTotalAttack());
            _view.UpdateEnemyStats(_opponent.TeamController.GetTotalHealth(), _opponent.TeamController.GetTotalAttack());
        }

    }
}