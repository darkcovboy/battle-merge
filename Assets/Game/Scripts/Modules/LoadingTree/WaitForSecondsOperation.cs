using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Modules.LoadingTree
{
    [Serializable]
    public class WaitForSecondsOperation : LoadingOperation
    {
        [SerializeField]
        private float seconds;

        public override float Progress => _progress;

        private float _progress;
        
        public override async UniTask<Result> Run(LoadingBundle bundle)
        {
            await this.WaitForSeconds();
            return Result.Success();
        }

        private IEnumerator WaitForSeconds()
        {
            float currentTime = 0;
            _progress = 0;
           
            while (currentTime < this.seconds)
            {
                yield return null;
                currentTime += Time.deltaTime;
                _progress = currentTime / this.seconds;
            }

            _progress = 1.0f;
        }
    }
}