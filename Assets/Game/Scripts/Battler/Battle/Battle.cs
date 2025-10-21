using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Battler.Monsters;
using UnityEngine;

namespace Game.Scripts.Battler.Battle
{
    public class Battle
    {
        private readonly ICombat _teamA;
        private readonly ICombat _teamB;

        public event Action<Battle> OnBattleEnded;

        private bool _active;

        public Battle(ICombat teamA, ICombat teamB)
        {
            _teamA = teamA;
            _teamB = teamB;
        }

        public bool Involves(ICombat c) => c == _teamA || c == _teamB;

        public void Start()
        {
            _active = true;

            _teamA.IsInBattle = true;
            _teamB.IsInBattle = true;

            LookAtEachOther();
            
            _teamA.OnBattleStarted(_teamB);
            _teamB.OnBattleStarted(_teamA);

            foreach (var m in _teamA.Team.Concat(_teamB.Team))
                m.OnDeath += OnMonsterDeath;

            AssignTargets();
        }

        private void LookAtEachOther()
        {
            Vector3 dir = (_teamB.Transform.position - _teamA.Transform.position).normalized;

            _teamA.Transform.rotation = Quaternion.LookRotation(dir);
            _teamB.Transform.rotation = Quaternion.LookRotation(-dir);
        }

        private void AssignTargets()
        {
            foreach (var m in _teamA.Team)
                if (!m.IsDead)
                    m.SetTarget(GetRandomAlive(_teamB.Team));

            foreach (var m in _teamB.Team)
                if (!m.IsDead)
                    m.SetTarget(GetRandomAlive(_teamA.Team));
        }

        private void OnMonsterDeath(MonsterCharacter dead)
        {
            if (!_active) return;

            bool teamADead = _teamA.Team.All(m => m.IsDead);
            bool teamBDead = _teamB.Team.All(m => m.IsDead);

            if (teamADead || teamBDead)
            {
                End(teamADead ? _teamB : _teamA);
            }
            else
            {
                AssignTargets();
            }
        }

        private void End(ICombat winner)
        {
            _active = false;

            foreach (var m in _teamA.Team.Concat(_teamB.Team))
                m.OnDeath -= OnMonsterDeath;
            
            _teamA.IsInBattle = false;
            _teamB.IsInBattle = false;

            _teamA.OnBattleEnded(winner == _teamA);
            _teamB.OnBattleEnded(winner == _teamB);

            OnBattleEnded?.Invoke(this);
        }

        private MonsterCharacter GetRandomAlive(IReadOnlyList<MonsterCharacter> list)
        {
            var alive = list.Where(m => !m.IsDead).ToList();
            return alive.Count > 0 ? alive[UnityEngine.Random.Range(0, alive.Count)] : null;
        }
    }
}