namespace Game.Scripts.Battler.Module.StateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
    }
}