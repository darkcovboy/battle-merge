namespace Game.Scripts.Battler.StateGame
{
    public interface ILoseHandler
    {
        void OnLose(LoseReason reason);
    }
}