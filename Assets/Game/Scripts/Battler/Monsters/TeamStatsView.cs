using Game.Scripts.Useful.Extensions;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters
{
    public class TeamStatsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _attackText;

        private MonsterTeamController _team;
        private Camera _camera;

        private float _currentHp;
        private float _currentAttack;

        public void Initialize(MonsterTeamController team)
        {
            _team = team;
            _camera = Camera.main;
            UpdateStats();

            foreach (var monster in team.Monsters)
            {
                monster.OnDeath += OnMonsterDeath;
            }
        }

        private void OnDestroy()
        {
            if (_team == null) return;
            foreach (var monster in _team.Monsters)
            {
                monster.OnDeath -= OnMonsterDeath;
            }
        }

        private void Update()
        {
            if (_camera)
                transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }

        private void OnMonsterDeath(MonsterCharacter monster)
        {
            UpdateStats();
        }

        private void UpdateStats()
        {
            _currentHp = _team.GetTotalHealth();
            _currentAttack = _team.GetTotalAttack();

            _hpText.text = $"{_currentHp.ToShortString()}";
            _attackText.text = $"{_currentAttack.ToShortString()}";
        }
    }
}