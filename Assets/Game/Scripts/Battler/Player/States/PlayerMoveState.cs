using Game.Scripts.Battler.Input;
using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Player.States
{
    public class PlayerMoveState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly PlayerView _view;
        private readonly IInput _input;

        public PlayerMoveState(StateMachine stateMachine, PlayerView view, IInput input)
        {
            _stateMachine = stateMachine;
            _view = view;
            _input = input;
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            var dir = _input.GetInputDirection();
            _view.Move(dir.normalized);
        }
    }
}