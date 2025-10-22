using UnityEngine;

namespace Game.Scripts.Battler.DamageViews
{
    public interface IDamagePopupService
    {
        void ShowDamage(Vector3 position, float amount, bool isPlayer);
    }

    public class DamagePopupService : IDamagePopupService
    {
        private readonly DamagePopupView _view;
        private readonly Color _playerColor;
        private readonly Color _enemyColor;
        private readonly DamagePopupPool _pool;

        public DamagePopupService(DamagePopupView view, int initialCount, Color playerColor, Color enemyColor)
        {
            _view = view;
            _playerColor = playerColor;
            _enemyColor = enemyColor;
            _pool = new DamagePopupPool(view, initialCount);
        }

        public void ShowDamage(Vector3 position, float amount, bool isPlayer)
        {
            var popup = _pool.Get();
            popup.transform.position = position;
            
            popup.Play(amount, isPlayer ? _playerColor : _enemyColor, () => _pool.Release(popup));
        }
    }
}