namespace Game.Scripts.Battler.StateGame
{
    public enum GameResult { None, Win, Lose }

    public enum WinReason { AllEnemiesDead, BossKilled }
    public enum LoseReason { PlayerDead }

    public interface IWinHandler
    {
        void OnWin(WinReason reason);
    }
}