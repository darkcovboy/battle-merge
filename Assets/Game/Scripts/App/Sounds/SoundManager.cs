using UnityEngine;
using UnityEngine.Audio;

namespace Game.Scripts.App.Sounds
{
    public class SoundManager : ISoundManager
    {
        private const string MUSIC_KEY = "MusicEnabled";
        private const string SOUND_KEY = "SoundEnabled";

        private readonly AudioMixer _musicMixer;
        private readonly AudioMixer _sfxMixer;

        private readonly string _musicParam;
        private readonly string _sfxParam;

        private bool _musicEnabled;
        private bool _soundEnabled;

        public bool IsMusicEnabled => _musicEnabled;
        public bool IsSoundEnabled => _soundEnabled;

        public SoundManager(AudioMixer musicMixer, AudioMixer sfxMixer,
            string musicParam = "MusicVolume", string sfxParam = "SFXVolume")
        {
            _musicMixer = musicMixer;
            _sfxMixer = sfxMixer;
            _musicParam = musicParam;
            _sfxParam = sfxParam;

            LoadSettings();
            ApplyMusicState();
            ApplySoundState();
        }

        private void LoadSettings()
        {
            _musicEnabled = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
            _soundEnabled = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;
        }

        public void ToggleMusic()
        {
            _musicEnabled = !_musicEnabled;
            PlayerPrefs.SetInt(MUSIC_KEY, _musicEnabled ? 1 : 0);
            ApplyMusicState();
        }

        public void ToggleSound()
        {
            _soundEnabled = !_soundEnabled;
            PlayerPrefs.SetInt(SOUND_KEY, _soundEnabled ? 1 : 0);
            ApplySoundState();
        }

        public void ApplyMusicState()
        {
            float volume = _musicEnabled ? 0f : -80f;
            _musicMixer.SetFloat(_musicParam, volume);
        }

        public void ApplySoundState()
        {
            float volume = _soundEnabled ? 0f : -80f;
            _sfxMixer.SetFloat(_sfxParam, volume);
        }
    }
}