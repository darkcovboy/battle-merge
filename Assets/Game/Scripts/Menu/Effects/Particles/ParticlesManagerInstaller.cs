using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Effects.Particles
{
    [CreateAssetMenu(
        fileName = "ParticlesManagerInstaller",
        menuName = "Zenject/New ParticlesManagerInstaller"
    )]
    public class ParticlesManagerInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ParticlesCatalog _particlesCatalog;
        
        public override void InstallBindings()
        {
            Container.Bind<ParticlesManager>()
                .AsSingle()
                .WithArguments(_particlesCatalog)
                .NonLazy();
        }
    }
}