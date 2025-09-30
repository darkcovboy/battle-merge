using UnityEngine;

namespace Game.Scripts.Menu.Effects.Particles
{
    [CreateAssetMenu(fileName = "ParticlesCatalog", menuName = "Configs/ParticlesCatalog")]
    public class ParticlesCatalog : ScriptableObject
    {
        [SerializeField] private DisappearingParticle _appearCharacterParticle;
        
        public DisappearingParticle AppearCharacterParticle => _appearCharacterParticle;
    }
}