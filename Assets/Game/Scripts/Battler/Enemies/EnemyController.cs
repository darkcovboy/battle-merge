using System.Collections.Generic;
using Game.Scripts.Battler.Battle;
using Game.Scripts.Battler.Monsters;
using Game.Scripts.Battler.Player;
using Game.Scripts.Battler.Player.Pokeballs;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Enemies
{
    public class EnemyController : MonoBehaviour, ICombat
    {
        [SerializeField] private EnemyView _view;
        [SerializeField] private BattleZone _battleZone;
        [SerializeField] private Pokeball _pokeballPrefab;
        [SerializeField] private Transform _pokeballSpawnPoint;

        [Header("Team Setup")]
        [SerializeField] private List<string> _monsterIds = new(); // вручную задаём ID монстров

        private MonsterTeamController _teamController;
        private MonsterFactory _factory;

        public IReadOnlyList<MonsterCharacter> Team => _teamController.Monsters;

        [Inject]
        public void Construct(MonsterFactory factory)
        {
            _factory = factory;
        }
        private void Awake()
        {
            _teamController = new MonsterTeamController(
                _factory,
                _monsterIds,
                transform,
                _battleZone,
                _pokeballPrefab,
                _pokeballSpawnPoint
            );

            _view.OnThrowAnimationComplete += OnThrowAnimationComplete;
        }

        private void OnDestroy()
        {
            _view.OnThrowAnimationComplete -= OnThrowAnimationComplete;
        }

        public void OnBattleStarted(ICombat opponent)
        {
            // у врага тоже проигрываем анимацию броска
            _view.PlayThrow();
        }

        public void OnBattleEnded(bool victory)
        {
            _teamController.ReturnMonsters();
        }

        private void OnThrowAnimationComplete()
        {
            _teamController.ThrowPokeballs();
        }
    }
}