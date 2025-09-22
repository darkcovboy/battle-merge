using UnityEngine;
using Zenject;

namespace Game.Scripts.Modules.Currency
{
    [CreateAssetMenu(
        fileName = "CurrencyInstaller",
        menuName = "Zenject/New CurrencyInstaller"
    )]
    public class CurrencyInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private CurrencyCell[] _cells;

        public override void InstallBindings()
        {
            Container.Bind<CurrencyBank>()
                .AsSingle()
                .WithArguments(_cells)
                .NonLazy();
        }
    }
}