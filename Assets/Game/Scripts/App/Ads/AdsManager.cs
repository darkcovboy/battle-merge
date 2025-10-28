using System;
using UnityEngine;

namespace Game.Scripts.App.Ads
{
    public class AdsManager : MonoBehaviour
    {
        public static AdsManager Instance {get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void ShowInterstitialAd()
        {
            
        }

        public void ShowRewardedAd(Action onReward)
        {
#if UNITY_EDITOR
            onReward?.Invoke();
            return;
#endif
        }
    }
}