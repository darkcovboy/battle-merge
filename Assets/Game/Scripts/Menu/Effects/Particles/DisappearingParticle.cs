using System;
using UnityEngine;

namespace Game.Scripts.Menu.Effects.Particles
{
    public class DisappearingParticle : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;

        public void Start() => Invoke(nameof(Destroy), _delay);

        public void Destroy() => Destroy(gameObject);
    }
}