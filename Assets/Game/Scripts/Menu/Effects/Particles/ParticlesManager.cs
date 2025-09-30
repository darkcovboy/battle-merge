using UnityEngine;

namespace Game.Scripts.Menu.Effects.Particles
{
    public class ParticlesManager
    {
        private readonly ParticlesCatalog _particlesCatalog;

        public ParticlesManager(ParticlesCatalog particlesCatalog)
        {
            _particlesCatalog = particlesCatalog;
        }

        public void PlayAppearCharacter(Vector3 position)
        {
            Object.Instantiate(_particlesCatalog.AppearCharacterParticle, position, Quaternion.identity);
        }
    }
}