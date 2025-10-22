using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.DamageViews
{
    public class DamagePopupInstaller : MonoInstaller
    {
        [SerializeField] private DamagePopupView _prefab;
        [SerializeField] private int _size = 15;
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _enemyColor;
        
        public override void InstallBindings()
        {
            Container.Bind<DamagePopupView>().FromInstance(_prefab).AsSingle();
            Container.BindInterfacesAndSelfTo<DamagePopupService>().AsSingle()
                .WithArguments(_size, _playerColor, _enemyColor);
        }
    }
}