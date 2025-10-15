using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Game.Scripts.Battler.Battle;
using Game.Scripts.Battler.Monsters;
using Game.Scripts.Battler.Player.Pokeballs;
using Game.Scripts.Menu.Field;
using UnityEngine;

namespace Game.Scripts.Battler.Player
{
    public class MonsterTeamController
    {
        private readonly MonsterFactory _factory;
        private readonly Field _field;
        private readonly List<MonsterCharacter> _monsters = new();
        private readonly Pokeball _pokeballPrefab;
        private readonly Transform _pokeballSpawnPoint;
        private readonly BattleZone _battleZone;
        private readonly PokeballPool _pokeballPool;
        private readonly Transform _playerTransform;

        
        public MonsterTeamController(MonsterFactory factory, Field field, Transform playerTransform, BattleZone battleZone, Pokeball pokeballPrefab, Transform pokeballSpawnPoint)
        {
            _factory = factory;
            _field = field;
            _battleZone = battleZone;
            _pokeballPrefab = pokeballPrefab;
            _pokeballSpawnPoint = pokeballSpawnPoint;
            _playerTransform = playerTransform;
            _pokeballPool = new PokeballPool(pokeballPrefab, _field.CharacterPositions.Values.Count);


            InitializeTeam();
        }

        private void InitializeTeam()
        {
            var monsters = _field.CharacterPositions.Values.ToList();

            for (int i = 0; i < monsters.Count; i++)
            {
                if (!string.IsNullOrEmpty(monsters[i]))
                {
                    var monster = _factory.Create(Vector3.zero, monsters[i]);
                    monster.gameObject.SetActive(false);
                    _monsters.Add(monster);
                }
            }
        }

        public void ThrowPokeballs()
        {
            for (int i = 0; i < _monsters.Count; i++)
            {
                var monster = _monsters[i];
                if (monster == null) continue;

                Vector3 targetPos = _battleZone.GetRandomPointInside();

                var pokeball = _pokeballPool.Get();
                pokeball.transform.position = _pokeballSpawnPoint.position;


                pokeball.Launch(targetPos,
                    (landPos) =>
                    {
                        monster.transform.position = landPos;
                        monster.gameObject.SetActive(true);
                        monster.ShowWithSpawnAnimation();
                    },
                    (ball) => _pokeballPool.Release(ball)
                );
            }
        }

        public void ReturnMonsters()
        {
            foreach (var monster in _monsters)
            {
                if (!monster.gameObject.activeSelf) continue;

                monster.HideWithDespawnAnimation(() =>
                {
                    var ball = _pokeballPool.Get();
                    ball.transform.position = monster.transform.position;

                    ball.transform.DOJump(_playerTransform.position, 2f, 1, 0.8f)
                        .OnComplete(() => _pokeballPool.Release(ball));
                });
            }
        }
    }
}