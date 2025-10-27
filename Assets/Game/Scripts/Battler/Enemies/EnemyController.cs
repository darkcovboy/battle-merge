using System;
using System.Collections.Generic;
using Game.Scripts.Battler.Battle;
using Game.Scripts.Battler.Enemies.StateMachine;
using Game.Scripts.Battler.Monsters;
using Game.Scripts.Battler.Player;
using Game.Scripts.Battler.Player.Pokeballs;
using Game.Scripts.Battler.StateGame;
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
        [SerializeField] private TeamStatsView _teamStatsView;

        [Header("Team Setup")]
        [SerializeField] private List<string> _monsterIds = new();

        private MonsterTeamController _teamController;
        private MonsterFactory _factory;
        private EnemyStateMachine _stateMachine;
        private MatchStateService _stateService;

        public IReadOnlyList<MonsterCharacter> Team => _teamController.Monsters;
        public Transform Transform => transform;
        public bool IsInBattle { get; set; }
        public MonsterTeamController TeamController => _teamController;
        public bool IsPlayer => false;

        [Inject]
        public void Construct(MonsterFactory factory,MatchStateService stateService)
        {
            _factory = factory;
            _stateService = stateService;
        }
        
        private void Awake()
        {
            _teamController = new MonsterTeamController(
                _factory,
                _monsterIds,
                transform,
                _battleZone,
                _pokeballPrefab,
                _pokeballSpawnPoint,
                IsPlayer
            );

            
            _view.OnThrowAnimationComplete += OnThrowAnimationComplete;
            _stateMachine = new EnemyStateMachine(this, _view);
            _teamStatsView.Initialize(_teamController);
            _stateService.OnEnemySpawned(new EnemyEvent{Id = GetInstanceID(), IsBoss = false});
        }

        private void Update()
        {
            if (IsInBattle)
                _view.SetIdle();
            else
                _stateMachine.Update();
        }

        private void OnDestroy()
        {
            _view.OnThrowAnimationComplete -= OnThrowAnimationComplete;
        }

        public void OnBattleStarted(ICombat opponent)
        {
            _view.PlayThrow();
            _teamStatsView.gameObject.SetActive(false);
        }

        public void OnBattleEnded(bool victory)
        {
            _stateMachine.ChangeState(victory
                ? EnemyStateMachine.EnemyStateType.Victory
                : EnemyStateMachine.EnemyStateType.Dead);

            if (!victory)
            {
                _battleZone.gameObject.SetActive(false);
                _stateService.OnEnemyDied(new EnemyEvent{Id = GetInstanceID(), IsBoss = false});
            }
            
            _teamController.ReturnMonsters();
            
            _teamStatsView.gameObject.SetActive(victory);
            
            Destroy(gameObject, 3f);
        }

        private void OnThrowAnimationComplete()
        {
            _teamController.ThrowPokeballs();
        }
    }
}