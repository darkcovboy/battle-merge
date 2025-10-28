namespace Game.Scripts.App.Sounds
{
    public interface ISoundManager
    {
        bool IsMusicEnabled { get; }
        bool IsSoundEnabled { get; }

        void ToggleMusic();
        void ToggleSound();

        void ApplyMusicState();
        void ApplySoundState();
    }
}