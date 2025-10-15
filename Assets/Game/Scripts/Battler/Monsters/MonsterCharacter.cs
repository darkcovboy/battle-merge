using System;
using DG.Tweening;
using Game.Scripts.App.Characters.Data;
using Game.Scripts.App.Characters.Menu;
using Game.Scripts.Battler.Monsters.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Monsters
{
    public class MonsterCharacter : MonoBehaviour, IMonsterTarget
    {
        [SerializeField] private MonsterView _view;
        [SerializeField] private GameObject _projectilePrefab;

        private MonsterStateMachine _stateMachine;
        private IMonsterTarget _target;
        private float _currentHealth;
        private CharacterConfig _config;
        private Transform _owner;
        
        private Vector3 _originalScale;


        public bool IsDead => _currentHealth <= 0;
        public CharacterConfig Config => _config;
        public Transform Transform => transform;
        public GameObject ProjectilePrefab => _projectilePrefab;

        private void Awake()
        {
            _originalScale = transform.localScale;
            transform.localScale = Vector3.zero;
            _stateMachine = new MonsterStateMachine(this, _view);
        }

        private void Update() => _stateMachine.Update();

        public void Initialize(CharacterConfig config)
        {
            _config = config;
            _currentHealth = _config.Health;
        }
        
        public void ShowWithSpawnAnimation()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(_originalScale, 0.4f).SetEase(Ease.OutBack);
        }

        public void HideWithDespawnAnimation(Action onComplete = null)
        {
            transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        public void SetTarget(IMonsterTarget target) => _target = target;

        public IMonsterTarget GetTarget() => _target;

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
        }
    }
}