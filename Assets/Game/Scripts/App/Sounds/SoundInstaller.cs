using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Game.Scripts.App.Sounds
{
    [CreateAssetMenu(
        fileName = "SoundInstaller",
        menuName = "Zenject/New SoundInstaller"
    )]
    public class SoundInstaller : ScriptableObjectInstaller
    {
        [Header("Audio Mixers")]
        [SerializeField] private AudioMixer _musicMixer;
        [SerializeField] private AudioMixer _sfxMixer;

        [Header("Parameter Names")]
        [SerializeField] private string _musicParam = "MusicVolume";
        [SerializeField] private string _sfxParam = "SFXVolume";

        public override void InstallBindings()
        {
            Container.Bind<ISoundManager>()
                .To<SoundManager>()
                .AsSingle()
                .WithArguments(_musicMixer, _sfxMixer, _musicParam, _sfxParam)
                .NonLazy();
        }
    }
}