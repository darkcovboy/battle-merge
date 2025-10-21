using System.Collections.Generic;
using System.Linq;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.Battler.Battle;
using Game.Scripts.Battler.Input;
using Game.Scripts.Battler.Monsters;
using Game.Scripts.Battler.Player.Pokeballs;
using Game.Scripts.Menu.Field;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Player
{
    public class PlayerCharacter : MonoBehaviour, ICombat
    {
        [SerializeField] private PlayerView _view;
        [SerializeField] private BattleZone _battleZone;
        [SerializeField] private Pokeball _pokeballPrefab;
        

        private IInput _input;
        private PlayerStateMachine _stateMachine;
        private MonsterTeamController _teamController;
        
        public IReadOnlyList<MonsterCharacter> Team => _teamController.Monsters;


        [Inject]
        public void Construct(IInput input, MonsterFactory factory, Field field)
        {
            _input = input;
            _stateMachine = new PlayerStateMachine(_view, _input);
            _teamController = new MonsterTeamController(factory, field, transform, _battleZone, _pokeballPrefab, _view.PokeballPosition);

            _view.OnThrowAnimationComplete += OnThrowAnimationComplete;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnDestroy()
        {
            _view.OnThrowAnimationComplete -= OnThrowAnimationComplete;
        }

        public void OnBattleStarted(ICombat opponent)
        {
            _input.SetBlocked(true);
            _view.PlayThrow();
        }

        public void OnBattleEnded(bool victory)
        {
            _input.SetBlocked(false);
            _teamController.ReturnMonsters();
        }

        private void OnThrowAnimationComplete()
        {
            _teamController.ThrowPokeballs();
        }
    }
}