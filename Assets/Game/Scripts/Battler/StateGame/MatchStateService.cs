using System;
using UniRx;
using UnityEngine;

namespace Game.Scripts.Battler.StateGame
{
    public class MatchStateService : IDisposable
    {
        private readonly Subject<EnemyEvent> _enemySpawned = new();
        private readonly Subject<EnemyEvent> _enemyDied = new();
        private readonly Subject<PlayerEvent> _playerDied = new();

        private readonly ReactiveProperty<GameResult> _result = new(GameResult.None);
        public IReadOnlyReactiveProperty<GameResult> Result => _result;
        private readonly ReactiveProperty<int> _enemiesKilled = new(0);
        private readonly ReactiveProperty<int> _totalEnemies = new(0);

        
        private int _aliveEnemies;
        private bool _bossAlive;
        private bool _finished;

        private readonly CompositeDisposable _disposables = new();
        
        public IReadOnlyReactiveProperty<int> EnemiesKilled => _enemiesKilled;
        public IReadOnlyReactiveProperty<int> TotalEnemies => _totalEnemies;


        public MatchStateService()
        {
            _enemySpawned
                .Where(_ => !_finished)
                .Subscribe(e =>
                {
                    _aliveEnemies++;
                    _totalEnemies.Value++;
                    if (e.IsBoss) _bossAlive = true;
                })
                .AddTo(_disposables);

            _enemyDied
                .Where(_ => !_finished)
                .Subscribe(e =>
                {
                    _enemiesKilled.Value++;
                    if (e.IsBoss)
                    {
                        _bossAlive = false;
                        FinishWin(WinReason.BossKilled);
                        return;
                    }

                    _aliveEnemies = Mathf.Max(0, _aliveEnemies - 1);
                    if (_aliveEnemies == 0)
                        FinishWin(WinReason.AllEnemiesDead);
                })
                .AddTo(_disposables);

            _playerDied
                .Where(_ => !_finished)
                .Subscribe(_ => FinishLose(LoseReason.PlayerDead))
                .AddTo(_disposables);
        }

        public void OnEnemySpawned(EnemyEvent e) => _enemySpawned.OnNext(e);
        public void OnEnemyDied(EnemyEvent e) => _enemyDied.OnNext(e);
        public void OnPlayerDied(PlayerEvent e) => _playerDied.OnNext(e);

        private void FinishWin(WinReason reason)
        {
            if (_finished) return;
            _finished = true;
            _result.Value = GameResult.Win;
            Debug.Log($"Win: {reason}");
        }

        private void FinishLose(LoseReason reason)
        {
            if (_finished) return;
            _finished = true;
            _result.Value = GameResult.Lose;
            Debug.Log($"Lose: {reason}");
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _enemySpawned.Dispose();
            _enemyDied.Dispose();
            _playerDied.Dispose();
        }
    }
}