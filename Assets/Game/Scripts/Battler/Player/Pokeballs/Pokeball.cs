using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Battler.Player.Pokeballs
{
    public class Pokeball : MonoBehaviour
    {
        private Action<Vector3> _onLanded;
        private Action<Pokeball> _onReturnToPool;

        public void Launch(Vector3 target, Action<Vector3> onLanded, Action<Pokeball> onReturnToPool)
        {
            _onLanded = onLanded;
            _onReturnToPool = onReturnToPool;

            Vector3 start = transform.position;
            Vector3 mid = (start + target) / 2 + Vector3.up * 2f;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOPath(new Vector3[] { mid, target }, 0.8f, PathType.CatmullRom))
                .Join(transform.DORotate(new Vector3(720, 720, 0), 0.8f, RotateMode.FastBeyond360))
                .OnComplete(() =>
                {
                    _onLanded?.Invoke(target);
                    ReturnToPool();
                });
        }

        public void ResetState()
        {
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
            transform.DOKill();
        }

        private void ReturnToPool()
        {
            ResetState();
            _onReturnToPool?.Invoke(this);
        }
    }
}