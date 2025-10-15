namespace Game.Scripts.Battler.Module.StateMachine
{
    public class BaseState : IState
    {
        protected readonly StateMachine _stateMachine;

        protected BaseState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        public void AddTransition(BaseState toState, ITransition condition) => _stateMachine.AddTransition(this, toState, condition);
    }
}